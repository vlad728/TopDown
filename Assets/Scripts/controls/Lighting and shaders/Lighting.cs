using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Lighting : MonoBehaviour
{

    public Light globalLight;
    private float duration = 1f;
    public bool OnOff = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            OnOff = !OnOff;
            StartCoroutine(SmoothLighting());
        }
    }

    IEnumerator SmoothLighting()
    {
        float start = globalLight.intensity;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float x = timeElapsed / duration;
            if (OnOff)
            {
                globalLight.intensity = Mathf.Lerp(start, 0, x);
                yield return null;
            }
            else
            {
                globalLight.intensity = Mathf.Lerp(start, 10, x);
                yield return null;
            }
        }
        if (OnOff)
        {
            globalLight.intensity = 0;
        }
        else
        {
            globalLight.intensity = 10;
        }

    }

}
