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
        animator.SetBool(nameof(isGameStart), isGameStart);
        animator.SetInteger(nameof(dance), dance);
        animator.SetInteger(nameof(die), die);
    }
    public void SetDustParticle(bool setActiveValue)
    {
        particleDust.SetActive(setActiveValue);
    }
}
