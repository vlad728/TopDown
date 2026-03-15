using UnityEngine;

public class PlayerAim : MonoBehaviour
{


    //misc
    public bool isShooting = false;
    public Camera camera;
    private Vector3 mouse;

    //Attributes
    private float smoothSpeed = 5f;
    private float offSetFactor = 0.1f;
    public float rotationSpeed = 1.25f;
    //Targets
    private Vector3 directionTarget;
    private Quaternion rotatationTarget;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        PlayerRotation();
        CameraFollow();
    }

    private void PlayerRotation()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mouse = hit.point;
        }
        directionTarget = (mouse - transform.position).normalized;
        directionTarget.y = 0;
        rotatationTarget = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotatationTarget, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void CameraFollow()
    {
        Vector3 targetPos = transform.position + (mouse - transform.position) * offSetFactor;
        targetPos.y = camera.transform.position.y;
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, Time.deltaTime * smoothSpeed);
    }

}


