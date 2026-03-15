using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    private bool canShoot = true;

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
