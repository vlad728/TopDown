using UnityEngine;
using UnityEngine.AI;

public class EnemyControls : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EnemyDeath>().KillEnemy();
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<ShootControls>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<AudioController>().enabled = false;
            collision.gameObject.GetComponent <PlayerAim>().enabled = false;
        }
    }

}
