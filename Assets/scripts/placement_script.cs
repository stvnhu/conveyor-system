using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class placement_script : MonoBehaviour
{
    [SerializeField] private GameObject conveyor;
    [SerializeField] private GameObject conveyor_short;
    [SerializeField] private GameObject conveyor_incline;
    [SerializeField] private GameObject box_spawner;
    [SerializeField] private GameObject can_spawner;

    [SerializeField] private LayerMask preview_layer;
    private int preview_layer_num;

    private Camera cam;
    private int raycast_dist;

    private List<GameObject> placed; // list of placed objects
    private GameObject selected; // can either be prefab or placed object
    private MeshRenderer selected_box; // selected object indicator
    private GameObject clone; // object placement indicator

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        preview_layer_num = LayerMask.NameToLayer("placement_preview");
        cam = Camera.main;
        raycast_dist = 100;
        placed = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool success = Physics.Raycast(ray, out RaycastHit hit, raycast_dist, ~preview_layer);

        if (success)
        {
            if (clone != null)
            {
                Vector3 world_pos = hit.point;
                world_pos += new Vector3(0, selected.transform.position.y / 2, 0);
                clone.transform.position = world_pos;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (placed.Contains(hit.transform.gameObject))
                {
                    ClearSelected();
                    selected = hit.transform.gameObject;
                    GameObject child = selected.transform.Find("selection_box").gameObject;
                    selected_box = child.GetComponent<MeshRenderer>();
                    selected_box.enabled = true;
                }
                else if (clone != null)
                {
                    GameObject new_obj = Instantiate(selected, clone.transform.position, clone.transform.rotation);
                    placed.Add(new_obj);
                    ClearSelected();
                }
                else
                {
                    ClearSelected();
                }
            }
        }
    }

    void ClearSelected()
    {
        selected = null;
        Destroy(clone);
        clone = null;
        if (selected_box != null)
        {
            selected_box.enabled = false;
            selected_box = null;
        }
    }

    void SelectPrefab(GameObject obj)
    {
        if (selected == obj)
        {
            ClearSelected();
        }
        else
        {
            ClearSelected();
            selected = obj;
            clone = Instantiate(selected);
            foreach (Transform child in clone.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = preview_layer_num;
            }
        }
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (clone != null)
            {
                clone.transform.rotation = Quaternion.Euler(0, 90, 0) * clone.transform.rotation;
            }
        }
    }

    public void Delete(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selected_box != null)
            {
                placed.Remove(selected);
                Destroy(selected);
                ClearSelected();
            }
        }
    }

    public void One(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor);
        }
    }

    public void Two(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor_short);
        }
    }

    public void Three(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor_incline);
        }
    }

    public void Four(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(box_spawner);
        }
    }

    public void Five(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(can_spawner);
        }
    }
}
