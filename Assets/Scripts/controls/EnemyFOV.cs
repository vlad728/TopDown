using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;


public class EnemyFOV : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public Transform[] wayPoints;
    private int wayPointIndex = 0;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private Animator animator;


    public bool canSeePlayer;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPoints[wayPointIndex].position);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(Stay());
        StartCoroutine(FOVRoutine());
    }

    private void Update()
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

    private IEnumerator Stay()
    {
        animator.SetBool("front", false);
        yield return new WaitForSeconds(1);
        animator.SetBool("front", true);
        agent.SetDestination(wayPoints[wayPointIndex].position);
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
                agent.SetDestination(playerRef.transform.position);
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
}
