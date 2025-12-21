using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 5;
    private float vertical;
    private float horizontal;
    private Vector3 mousePos;
    private Vector2 direction;
    public LayerMask groundLayer;

    void Start()
    {
        
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * vertical * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontal * speed);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.up = mousePosition - new Vector2(transform.position.x, transform.position.y);

    }
}
