using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10;
    private float vertical;
    private float horizontal;
    public LayerMask groundLayer;
    public bool isDie = false;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        if (isDie)
        {
            return;
        }
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (isDie)
        {
            return;
        }
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        rb.linearVelocity = movement * speed;
    }
}
