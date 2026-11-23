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

    private int raycast_dist;

    public List<GameObject> placed; // list of placed objects
    public List<Vector3> placed_pos; // list of positions of placed objects
    private GameObject selected; // can either be prefab or placed object
    private MeshRenderer selected_box; // selected object indicator
    private GameObject clone; // object placement indicator
    private snap_point clone_sp; // snap point for detecting if clone is snapped
    private conveyor_script clone_cs;
    private float height_offset; // height of object to offset from ground
    public int rotation; // clone rotation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycast_dist = 100;
        placed = new List<GameObject>();
        height_offset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool success = Physics.Raycast(ray, out RaycastHit hit, raycast_dist, ~preview_layer);

        if (!success)
        {
            return;
        }

        if (clone != null)
        {
            bool found_sp = false;
            if (clone_sp != null)
            {
                clone_sp.hovering_sp = false;
            }

            Vector3 world_pos = hit.point;
            world_pos += new Vector3(0, height_offset / 2, 0);
            clone.transform.position = world_pos;

            foreach (GameObject obj in placed)
            {
                if (Vector3.Distance(world_pos, obj.transform.position) > 5)
                {
                    continue;
                }
                snap_point placed_sp = obj.GetComponent<snap_point>();
                foreach (Vector3 p in placed_sp.GetPoints(clone.name, rotation))
                {
                    if (placed_pos.Contains(p))
                    {
                        continue;
                    }

                    if (Vector3.Distance(p, world_pos) < 1.5f)
                    {
                        clone_sp.hovering_sp = true;
                        clone.transform.position = p;
                        found_sp = true;
                        break;
                    }
                }
                if (found_sp)
                {
                    break;
                }
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (clone != null)
            {
                GameObject box = clone.transform.Find("selection_box").gameObject;
                selection_box_script script = box.GetComponent<selection_box_script>();
                if (script.can_place)
                {
                    GameObject new_obj = Instantiate(selected, clone.transform.position, clone.transform.rotation);
                    new_obj.name = selected.name;
                    snap_point sp = new_obj.GetComponent<snap_point>();
                    sp.rotation = rotation;
                    conveyor_script cs = new_obj.GetComponent<conveyor_script>();
                    if (cs != null)
                    {
                        cs.direction = clone_cs.direction;
                    }
                    placed.Add(new_obj);
                    placed_pos.Add(new_obj.transform.position);
                    ClearSelected();
                }
            }
            else if (placed.Contains(hit.transform.root.gameObject))
            {
                ClearSelected();
                selected = hit.transform.root.gameObject;
                GameObject box = selected.transform.Find("selection_box").gameObject;
                selected_box = box.GetComponent<MeshRenderer>();
                selected_box.enabled = true;
            }
            else
            {
                ClearSelected();
            }
        }
    }

    void ClearSelected()
    {
        selected = null;
        Destroy(clone);
        clone = null;
        clone_sp = null;
        clone_cs = null;
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
            clone.name = selected.name + "_preview";
            clone_sp = clone.GetComponent<snap_point>();
            clone_cs = clone.GetComponent<conveyor_script>();
            foreach (Transform child in clone.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("placement_preview");
            }
            foreach (BoxCollider bc in clone.GetComponentsInChildren<BoxCollider>())
            {
                bc.enabled = false;
            }
            rotation = 0;
        }
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (clone != null)
            {
                ++rotation;
                clone.transform.rotation = Quaternion.Euler(0, 90, 0) * clone.transform.rotation;
                if (clone.name != "conveyor_incline_preview")
                {
                    if (clone.transform.eulerAngles.y >= 180)
                    {
                        clone.transform.rotation = Quaternion.Euler(0, 180, 0) * clone.transform.rotation;
                        rotation %= 2;
                    }
                }
                else
                {
                    rotation %= 4;
                }
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
                placed_pos.Remove(selected.transform.position);
                Destroy(selected);
                ClearSelected();
            }
        }
    }

    public void Reverse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (clone != null)
            {
                clone_cs.direction *= -1;
            }
            else if (selected != null)
            {
                conveyor_script cs = selected.GetComponent<conveyor_script>();
                if (cs != null)
                {
                    cs.direction *= -1;
                }
            }
        }
    }

    public void GlobalReverse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (GameObject obj in placed)
            {
                conveyor_script cs = obj.GetComponent<conveyor_script>();
                if (cs != null)
                {
                    cs.direction *= -1;
                }
            }
        }
    }

    public void One(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor);
            height_offset = 0.9546666f;
        }
    }

    public void Two(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor_short);
            height_offset = 0.9546667f;
        }
    }

    public void Three(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(conveyor_incline);
            height_offset = 1.297904f;
        }
    }

    public void Four(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(box_spawner);
            height_offset = 1;
        }
    }

    public void Five(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SelectPrefab(can_spawner);
            height_offset = 1;
        }
    }
}
