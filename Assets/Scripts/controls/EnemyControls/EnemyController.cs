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

    //Shooting
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletVel = 70;
    private bool canShoot = true;

    //Models
    public GameObject model;
    public GameObject model2;

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
        agent.updateRotation = true;
        agent.SetDestination(wayPoints[wayPointIndex].position);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(Stay());
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {

        if(canSeePlayer && agent.enabled)
        {
            Vector3 direction = (playerRef.transform.position - transform.position).normalized;
            direction.y = 0f;

            float distance = Vector3.Distance(transform.position, playerRef.transform.position);

            agent.updateRotation = false;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

            if (distance < keepDistance)
            {
                Vector3 retreatDestination = transform.position - direction * keepDistance;
                agent.SetDestination(retreatDestination);
            }
            else if (distance > keepDistance * 1.5f)
            {
                agent.SetDestination(playerRef.transform.position);
            }
            else
            {
                agent.ResetPath();
            }
            Shoot();
        }
        else
        {
            agent.updateRotation = true;
        }

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

    private void Shoot()
    {
        if (canShoot)
        {
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = firePoint.position + firePoint.forward * 50;
            }
            Debug.DrawRay(firePoint.position, firePoint.forward * 50, Color.green, 0.1f);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            StartCoroutine(MoveBullet(bullet.transform, firePoint.position, targetPoint));
            StartCoroutine(ShootCd());
        }
    }

    IEnumerator MoveBullet(Transform bulletTransform, Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(startPos, endPos);
        float lifeTime = distance / bulletVel;
        float timer = 0;
        while (timer < lifeTime && bulletTransform != null)
        {
            timer += Time.deltaTime;
            bulletTransform.position = Vector3.Lerp(startPos, endPos, timer / lifeTime);
            yield return null;
        }
        if (bulletTransform != null)
        {
            Destroy(bulletTransform.gameObject);
        }
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

    IEnumerator ShootCd()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (canSeePlayer && agent.enabled == true)
            {
                if (Vector3.Distance(transform.position, playerRef.transform.position) <= keepDistance)
                {
                    agent.ResetPath();
                    agent.updateRotation = false;
                }
                else
                {
                    agent.updateRotation = true;
                    agent.SetDestination(playerRef.transform.position);
                }
                StopCoroutine(Stay());
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
