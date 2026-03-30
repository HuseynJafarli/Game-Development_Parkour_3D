using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveForce = 15f;
    public float jumpForce = 6f;
    public float maxVelocity = 10f;
    public float fallThreshold = -10f;

    Rigidbody rb;
    bool isGrounded;
    float h, v;
    Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startPosition = transform.position;
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }

        if (transform.position.y < fallThreshold)
        {
            TeleportToStart();
        }
    }

    public void TeleportToStart()
    {
        transform.position = startPosition; 
        rb.linearVelocity = Vector3.zero;        
        rb.angularVelocity = Vector3.zero; 
    }

    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (rb.linearVelocity.magnitude < maxVelocity)
        {
            rb.AddForce(moveDir * moveForce);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}