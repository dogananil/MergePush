using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;


    public IEnumerator ScaleAnimation()
    {
        float timeLapse = 0.0f;
        float totalTime = 5f;

        while (timeLapse <= totalTime)
        {
            this.transform.localScale = Vector3.one * animationCurve.Evaluate(timeLapse / totalTime);
            timeLapse += Time.deltaTime;
            yield return null;
        }
        this.transform.localScale = Vector3.zero;

        this.gameObject.SetActive(false);
    }
    private void Start()
    {
       StartCoroutine(ScaleAnimation());
    }
}
