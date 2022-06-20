using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static bool shaking;
    Vector3 originalPos;
    private void Start()
    {
        originalPos= transform.localPosition;
    }
    public IEnumerator Shake(float duration,float magnitude)
    {
        
        float elapsed = 0.0f;
        transform.localPosition = originalPos;
        while(elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+ y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        shaking = false;
        transform.localPosition = originalPos;
    }
}
