using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMove : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 3f;
    public float airControl = 2f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;

    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        bool spacebar = Input.GetKeyDown(KeyCode.Space);

        Vector3 moveVector = (transform.forward * vInput) + (transform.right * hInput);

        // Normalize to a length of '1' to avoid speedy diagonals
        if (moveVector.magnitude > 1f)
            moveVector = moveVector.normalized;

        moveVector *= moveSpeed;

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2f + 0.1f, whatIsGround);

        float verticalSpeed = rb.linearVelocity.y;

        if (grounded)  // Player is able to walk if grounded
            rb.linearVelocity = new Vector3(moveVector.x, verticalSpeed, moveVector.z);
        else  // Control the player in the air
        {
            Vector3 desiredVecolity = new Vector3(moveVector.x, verticalSpeed, moveVector.z);
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, desiredVecolity, airControl * Time.deltaTime);
        }


        // Jump
        if (grounded && spacebar)
            rb.AddForce(Vector3.up * jumpForce);
    }
}
