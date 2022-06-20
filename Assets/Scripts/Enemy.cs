using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1;
    public Animator animator;
    public TextMeshPro text;
    public GameObject particleDust;

    public void ChangeAnimation(int die = -1, int dance = -1, bool isGameStart = false)
    {
        float random = Random.Range(0, 0.4f);
        StartCoroutine(AnimationNoise(random, isGameStart));
        animator.SetInteger(nameof(dance), dance);
        animator.SetInteger(nameof(die), die);
    }
    private IEnumerator AnimationNoise(float seconds, bool isGameStart = false)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool(nameof(isGameStart), isGameStart);
    }
    public void SetDustParticle(bool setActiveValue)
    {
        particleDust.SetActive(setActiveValue);
    }
    private void Punch()
    {
        
            
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.15f, 0.25f));
        


    }
}
