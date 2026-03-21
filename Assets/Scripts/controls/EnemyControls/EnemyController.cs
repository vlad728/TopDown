using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    //Navigation
    private float keepDistance = 15f;
    private NavMeshAgent agent;

    //Field of view
    public bool canSeePlayer;
    private bool isSeek = false;
    [Range(0, 360)]
    public float angle;
    public float radius;

    //Mapping
    public GameObject playerRef;
    public Transform[] wayPoints;
    public Transform myTarget;
    private int wayPointIndex = 0;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private Animator animator;

    private void Start()
    {
        myTarget = null;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPoints[wayPointIndex].position);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(Stay());
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (myTarget == null)
        {
            if (transform.position.x == wayPoints[wayPointIndex].position.x && transform.position.z == wayPoints[wayPointIndex].position.z)
            {
                wayPointIndex++;
                if (wayPointIndex >= wayPoints.Length)
                {
                    wayPointIndex = 0;
                }
                StartCoroutine(Stay());
            }
        }
        else
        {
            if (transform.position.x == myTarget.position.x && transform.position.z == myTarget.position.z)
            {
                StartCoroutine(SeekCoroutine());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isSeek == false)
        {
            if (other.gameObject.CompareTag("Sound"))
            {
                Seek(other.gameObject.transform);
                isSeek = true;
                StartCoroutine(SoundLife(other));
            }
        }
    }
    IEnumerator SoundLife(Collider other)
    {
        yield return new WaitForSeconds(10);
        Destroy(other.gameObject);
        isSeek = false;
    }

    private IEnumerator Stay()
    {
        animator.SetBool("front", false);
        yield return new WaitForSeconds(1);
        animator.SetBool("front", true);
        if (myTarget == null)
        {
            agent.SetDestination(wayPoints[wayPointIndex].position);
        }
        else
        {
            agent.SetDestination(myTarget.position);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (canSeePlayer && agent.enabled == true)
            {
                animator.SetBool("front", true);
                if (Vector3.Distance(transform.position, playerRef.transform.position) < keepDistance)
                { 
                    Vector3 distanceToPlayer = transform.position - playerRef.transform.position;
                    Vector3 retreatTarget = transform.position + distanceToPlayer.normalized * 2;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(retreatTarget, out hit, 3, NavMesh.AllAreas))
                    {
                        agent.stoppingDistance = 0;
                        agent.SetDestination(hit.position);
                    }
                    StopCoroutine(Stay());
                }
                else if (Vector3.Distance(transform.position, playerRef.transform.position) == keepDistance)
                {
                    StartCoroutine(Stay());
                }
                else
                {
                    agent.stoppingDistance = keepDistance;
                    agent.SetDestination(playerRef.transform.position);
                    StopCoroutine(Stay());
                }
            }
            else
            {
                FieldOfViewCheck();
            }
            
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    IEnumerator SeekCoroutine()
    {
        yield return new WaitForSeconds(3);
        myTarget = null;
        agent.SetDestination(wayPoints[wayPointIndex].position);
    }

    public void Seek(Transform target)
    {
        myTarget = target;
        agent.SetDestination(myTarget.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EnemyDeath>().KillEnemy();
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<ShootControls>().enabled = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<AudioController>().enabled = false;
            collision.gameObject.GetComponent<PlayerAim>().enabled = false;
            agent.SetDestination(wayPoints[wayPointIndex].position);
            canSeePlayer = false;
            StopCoroutine(FOVRoutine());
        }
    }
}
