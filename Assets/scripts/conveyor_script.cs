using UnityEngine;

public class conveyor_script : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float speed;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 1;
        direction = -transform.up;
    }

    void FixedUpdate()
    {
        Vector3 movement = speed * Time.fixedDeltaTime * direction;
        rb.position -= movement;
        rb.MovePosition(rb.position + movement);
    }
}
