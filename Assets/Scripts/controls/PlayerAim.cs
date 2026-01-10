using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAim : MonoBehaviour
{


    public float rotationSpeed = 1.25f;
    public bool isShooting = false;
    public Camera camera;
    private Vector3 mouse;
    private Vector3 directionTarget;
    private Quaternion rotatationTarget;

    private float reload = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();
    }
    private void PlayerRotation()
    {
        //Vector3 mousePosition = CheckMousePos();


        //Vector3 direction = mousePosition - transform.position;
        //direction.y = 0f; 


        //if (direction != Vector3.zero)
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(direction.normalized);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f), Time.deltaTime * rotationSpeed);
        //}


        //float horizontal = Input.GetAxis("Horizontal");
        //if (horizontal != 0f)
        //{
        //    transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        //}
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mouse = hit.point;
        }
        directionTarget = (mouse - transform.position).normalized;
        rotatationTarget = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotatationTarget, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

    }
    private Vector3 CheckMousePos()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        return mousePosition;
    }

}


