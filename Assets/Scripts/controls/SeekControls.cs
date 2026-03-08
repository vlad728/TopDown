using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SeekControls : MonoBehaviour
{

    private bool isSeek = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSeek == false)
        {
            if (other.gameObject.CompareTag("Sound"))
            {
                gameObject.transform.parent.gameObject.GetComponent<EnemyFOV>().Seek(other.gameObject.transform);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
