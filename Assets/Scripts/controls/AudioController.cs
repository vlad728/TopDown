using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    private bool canShoot = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            audioSource.Play();
            canShoot = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canShoot=true;
        }
    }
}
