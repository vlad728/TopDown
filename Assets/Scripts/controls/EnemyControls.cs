using UnityEngine;
using UnityEngine.AI;

public class EnemyControls : MonoBehaviour
{

    private Transform playerLocation;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("front", true);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerLocation.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EnemyDeath>().KillEnemy();
        }
    }

}
