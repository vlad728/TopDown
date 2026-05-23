using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, 23, player.position.z);
    }
}
