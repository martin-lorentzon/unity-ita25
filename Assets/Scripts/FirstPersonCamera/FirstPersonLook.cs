using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class FirstPersonLook : MonoBehaviour
{
    public float sensX = 1f;
    public float sensY = 1f;

    public new Transform camera;
    public float eyeHeight = 1f;

    //Private Variables
    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.visible = false;  // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Set the camera starting position
        Vector3 cameraTargetPosition = transform.position + (Vector3.up * eyeHeight);
        camera.position = cameraTargetPosition;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX;  // Rotate around Y-axis (vertical) to look left/right
        xRotation -= mouseY;  // Rotate around X-axis (horizontal) to look up/down

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limit the rotation

        // Set PlayerObject rotation and Camera rotation
        transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        camera.eulerAngles = new Vector3(xRotation, yRotation, 0f);

        // Move the camera
        Vector3 cameraTargetPosition = transform.position + (Vector3.up * eyeHeight);
        camera.position = Vector3.Lerp(camera.position, cameraTargetPosition, 0.5f);
    }
}
