using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class Character : MonoBehaviour
{
    public int level = 1;
    public Animator animator;
    public GameObject particleDust;
    public ParticleSystem particleLevelUp;
    public GameObject particleSpawn;

    public GameObject particleCrackLocation;

    public TextMeshPro TextMeshPro;

    public List<GameObject> outfits;
    [SerializeField] private ParticleSystem m_sweltParticle;

    public void ChangeAnimation(int die = -1, int dance = -1, bool isGameStart = false)
    {
        float random = Random.Range(0,0.4f);
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
    public void SetCrackParticle(bool setActiveValue)
    {
        particleCrackLocation.SetActive(setActiveValue);
        if (setActiveValue == true)
        {
            StartCoroutine(particleCrackLocation.GetComponent<CrackScript>().CreateCrackFx());
        }
    }
    public void SetLevelupParticle()
    {
        particleLevelUp.Play();
    }
    public void SetSpawnParticle(bool setActiveValue)
    {
        particleSpawn.SetActive(setActiveValue);
    }

    public void ResetCharacter()
    {
        foreach (var outfit in outfits)
        {
            outfit.SetActive(false);
        }
        transform.localScale = Vector3.one;
        level = 1;
        TextMeshPro.text = level.ToString();

        particleSpawn.SetActive(true);
        particleDust.SetActive(false);
        particleLevelUp.Play();
        ChangeAnimation(isGameStart:false);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    private void SwelteParticle()
    {
        m_sweltParticle.Play();
    }
    private void RightLegCrackParticle()
    {
        //m_sweltParticle.Play();
    }
    private void LeftLegCrackParticle()
    {
        m_sweltParticle.Play();
    }
    private void OnlyPushPose()
    {
        if(GameManager.speed==0)
        {
           // animator.SetFloat("pushSpeed", 0);
            animator.SetBool("canPush", false);
        }
        
    }
    private void Punch()
    {
        if(!CameraShake.shaking)
        {
            CameraShake.shaking = true;
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.15f, 0.25f));
        }
       

    }
}
