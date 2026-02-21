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
        }
    }

}
