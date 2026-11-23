using UnityEngine;

public class selection_box_script : MonoBehaviour
{
    private snap_point sp;
    private bool is_clone;
    private Material mat;
    public bool can_place;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject root = transform.root.gameObject;
        sp = root.GetComponent<snap_point>();

        can_place = true;

        if (root.name.EndsWith("_preview"))
        {
            is_clone = true;

            MeshRenderer mrend = gameObject.GetComponent<MeshRenderer>();
            mrend.enabled = true;

            Renderer rend = gameObject.GetComponent<Renderer>();
            mat = rend.material;
            mat.color = new Color(0, 1, 0, 0.2f);
        }
        else
        {
            is_clone = false;
        }
    }

    void FixedUpdate()
    {
        if (mat != null)
        {
            mat.color = new Color(0, 1, 0, 0.2f);
            can_place = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!sp.hovering_sp && is_clone && collider.name == "selection_box")
        {
            mat.color = new Color(1, 0, 0, 0.2f);
            can_place = false;
        }
    }
}
