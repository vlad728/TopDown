using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public LayerMask hitMask;
    private float laserLength = 50f;
    private LineRenderer lineRenderer;
    private Vector3 startPos;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        startPos = transform.position;
        Vector3 direction = transform.forward;
        
        direction.y = 0;
        direction.Normalize();
        Vector3 laserEnd;
        Ray laserRay = new Ray(startPos, direction);

        if (Physics.Raycast(laserRay, out RaycastHit laserhit, Mathf.Infinity, hitMask))
        {
            laserEnd = laserhit.point;
        }
        else
        {
            laserEnd = startPos + direction * laserLength;
        }

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, laserEnd);
    }
}
