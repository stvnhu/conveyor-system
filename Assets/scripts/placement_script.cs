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

    private GameObject selected; // selected prefab for placement
    private GameObject clone; // clone for placement display

    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        preview_layer_num = LayerMask.NameToLayer("placement_preview");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != null)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, 100, ~preview_layer))
            {
                Vector3 world_pos = hit.point;
                world_pos += new Vector3(0, selected.transform.position.y / 2, 0);
                clone.transform.position = world_pos;
            }
            if (Mouse.current.leftButton.isPressed)
            {
                clone = Instantiate(selected, clone.transform.position, clone.transform.rotation);
                ClearSelected();
            }
        }
    }

    void SetLayerRecursive(GameObject obj, int layer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    void ClearSelected()
    {
        selected = null;
        Destroy(clone);
        clone = null;
    }

    void SelectObject(GameObject opt)
    {
        if (selected == opt)
        {
            ClearSelected();
        } 
        else
        {
            if (selected != null)
            {
                ClearSelected();
            }

            selected = opt;
            clone = Instantiate(selected);
            SetLayerRecursive(clone, preview_layer_num);
        }
    }

    public void One(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectObject(conveyor);
        }
    }

    public void Two(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectObject(conveyor_short);
        }
    }

    public void Three(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectObject(conveyor_incline);
        }
    }

    public void Four(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectObject(box_spawner);
        }
    }

    public void Five(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectObject(can_spawner);
        }
    }
}
