using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10;
    private float vertical;
    private float horizontal;
    public LayerMask groundLayer;

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * vertical * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontal * speed);
    }
}
