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

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement != Vector3.zero)
        {
            movement = movement.normalized;
        }
        //rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
        rb.linearVelocity = movement * speed;
    }
}
