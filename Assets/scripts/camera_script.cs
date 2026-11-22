using UnityEngine;
using UnityEngine.InputSystem;

public class camera_script : MonoBehaviour
{
    // camera rotation
    private float sensitivity;
    private float min_pitch;
    private float max_pitch;
    private float yaw;
    private float pitch;

    // camera position
    private float move_speed;
    private bool moving_forward;
    private bool moving_backward;
    private bool moving_left;
    private bool moving_right;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = 0.7f;
        min_pitch = -70;
        max_pitch = 70;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;

        move_speed = 10;
        moving_forward = false;
        moving_backward = false;
        moving_left = false;
        moving_right = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            yaw += delta.x * sensitivity;
            pitch -= delta.y * sensitivity;
            pitch = Mathf.Clamp(pitch, min_pitch, max_pitch);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }

        if (moving_forward && moving_backward)
        { }
        else if (moving_forward)
        {
            transform.position += move_speed * Time.deltaTime * transform.forward;
        } else if (moving_backward)
        {
            transform.position -= move_speed * Time.deltaTime * transform.forward;
        }

        if (moving_left && moving_right)
        { }
        else if (moving_left)
        {
            transform.position -= move_speed * Time.deltaTime * transform.right;
        }
        else if (moving_right)
        {
            transform.position += move_speed * Time.deltaTime * transform.right;
        }
    }

    public void Forward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving_forward = true;
        }
        if (context.canceled)
        {
            moving_forward = false;
        }
    }

    public void Backward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving_backward = true;
        }
        if (context.canceled)
        {
            moving_backward = false;
        }
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving_left = true;
        }
        if (context.canceled)
        {
            moving_left = false;
        }
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving_right = true;
        }
        if (context.canceled)
        {
            moving_right = false;
        }
    }
}
