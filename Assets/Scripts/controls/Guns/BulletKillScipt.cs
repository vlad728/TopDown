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
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EnemyDeath>().KillEnemy();
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            other.gameObject.transform.parent.gameObject.GetComponent<ShootControls>().enabled = false;
            other.gameObject.transform.parent.gameObject.GetComponent<AudioController>().enabled = false;
            other.gameObject.GetComponent<PlayerAim>().enabled = false;
        }
    }
}
