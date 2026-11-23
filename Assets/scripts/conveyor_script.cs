using UnityEngine;

public class conveyor_script : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject arrow;
    private float speed;
    private Vector3 direction;
    public int dir = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2;
        direction = transform.up;
    }

    void FixedUpdate()
    {
        Vector3 movement = speed * Time.fixedDeltaTime * direction;
        rb.position -= movement;
        rb.MovePosition(rb.position + movement);
        Vector3 scale = arrow.transform.localScale;
        if (dir == 0)
        {
            scale.y = -Mathf.Abs(scale.y);
            direction = -transform.up;
        }
        else
        {
            scale.y = Mathf.Abs(scale.y);
            direction = transform.up;
        }
        arrow.transform.localScale = scale;
    }
}
