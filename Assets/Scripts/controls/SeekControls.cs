using UnityEngine;
using UnityEngine.AI;

public class SeekControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sound"))
        {
            gameObject.transform.parent.gameObject.GetComponent<EnemyFOV>().Seek(other.gameObject.transform);
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
