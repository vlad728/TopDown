using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class BulletKillScipt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyDeath>().KillEnemy();
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Destroy(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
