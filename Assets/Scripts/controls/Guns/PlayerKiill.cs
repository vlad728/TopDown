using UnityEngine;

public class PlayerKiill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EnemyDeath>().KillEnemy();
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            other.gameObject.GetComponent<ShootControls>().enabled = false;
            other.gameObject.GetComponent<AudioController>().enabled = false;
            other.gameObject.transform.GetChild(0).GetComponent<PlayerAim>().enabled = false;
            other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<LineRenderer>().enabled = false;
        }
    }
}
