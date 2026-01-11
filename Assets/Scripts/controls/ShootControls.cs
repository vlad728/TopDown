using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootControls : MonoBehaviour
{
    public Transform firePoint;

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
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }

        }
        Debug.DrawRay(firePoint.position, firePoint.forward * 50, Color.green, 0.1f);
    }

}
