using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPos = new Vector3(player.position.x, 23, player.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
