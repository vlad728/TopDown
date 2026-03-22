using UnityEngine;

public class ModelController : MonoBehaviour
{

    public bool isActive;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void LookAtPlayer()
    {
        if (isActive)
        {
            Quaternion targetRot = Quaternion.LookRotation((player.position - transform.position).normalized);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRot, Time.deltaTime * 700);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }
}
