using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootControls : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletVel = 70;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50))
        {
            //if (hit.collider.CompareTag("Enemy"))
            //{
            //    Destroy(hit.collider.gameObject);
            //}
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = firePoint.position + firePoint.forward * 50;
        }
        Debug.DrawRay(firePoint.position, firePoint.forward * 50, Color.green, 0.1f);
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(MoveBullet(bullet.transform, firePoint.position, targetPoint));
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
}
