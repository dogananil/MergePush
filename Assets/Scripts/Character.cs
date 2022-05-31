using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Character : MonoBehaviour
{
    public int level = 1;
    public Animator animator;
    public void ChangeAnimation(int die = -1, int dance = -1, bool isGameStart = false)
    {
        animator.SetBool(nameof(isGameStart), isGameStart);
        animator.SetInteger(nameof(dance), dance);
        animator.SetInteger(nameof(die), die);
    }
}
