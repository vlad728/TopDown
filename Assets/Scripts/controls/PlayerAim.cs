using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAim : MonoBehaviour
{


    public float rotationSpeed = 10f;
    public bool isShooting = false;

    private float reload = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();
    }
    private void PlayerRotation()
    {
        Vector3 mousePosition = CheckMousePos();


        Vector3 direction = mousePosition - transform.position;
        direction.y = 0f; 


        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f), Time.deltaTime * rotationSpeed);
        }


        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0f)
        {
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }
    }
    private Vector3 CheckMousePos()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        return mousePosition;
    }

}


