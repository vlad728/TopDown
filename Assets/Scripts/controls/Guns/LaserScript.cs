using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public LayerMask hitMask;
    private float laserLength = 50f;
    private LineRenderer lineRenderer;
    private Vector3 offset;
    private Vector3 startPos;

    void Start()
    {
        offset = new Vector3(0, 0, 0);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        startPos = transform.position + offset;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 target;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hitMask))
        {
            target = hit.point;
        }
        else
        {
            target = ray.GetPoint(laserLength);
        }
        Vector3 direction = (target - startPos).normalized;
        direction.y = 0;
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
