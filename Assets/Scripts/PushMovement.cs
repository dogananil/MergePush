using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMovement : MonoBehaviour
{
    [SerializeField] private float pushConst;
    private void Start()
    {

    }
    void FixedUpdate()
    {
        if (GameManager.isGameStart == true && GameManager.isGameEnd == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameManager.speed*pushConst);
        }
    }

    public static void SetSpeed()
    {
        

        GameManager.speed = ((float)GameManager.ourPower - (float)GameManager.enemyPower + (float)GameManager.tapPower) /2;
        GameManager.speed = GameManager.speed / ((float)GameManager.ourPower + (float)GameManager.enemyPower);
        GameManager.speed = Mathf.Clamp(GameManager.speed, -1, 1);
        

        foreach(Character temp in GameManager.currentTeam)
        {
            
            if (GameManager.speed<0)
            {
                temp.animator.SetBool("canPush", true);

                temp.animator.SetFloat("pushSpeed", -1 + GameManager.speed * 3f);
            }
            else if(GameManager.speed>0)
            {
               
                temp.animator.SetBool("canPush", true);

                temp.animator.SetFloat("pushSpeed", 1 + GameManager.speed * 3f);

                Debug.Log("Animatio n speed  =  " + temp.animator.speed);
            }
            else
            {
                temp.animator.SetBool("canPush", false);
                temp.animator.SetFloat("pushSpeed", 1 + GameManager.speed * 3f);
            }
            
        }
        foreach (Enemy temp in GameManager.enemyTeam)
        {
            if (GameManager.speed < 0)
            {
                temp.animator.SetBool("canPush", true);

                temp.animator.SetFloat("pushSpeed", -1 + GameManager.speed * 3f);
            }
            else if (GameManager.speed > 0)
            {

                temp.animator.SetBool("canPush", true);

                temp.animator.SetFloat("pushSpeed", 1 + GameManager.speed * 3f);

                Debug.Log("Animatio n speed  =  " + temp.animator.speed);
            }
            else
            {
                temp.animator.SetBool("canPush", false);
                temp.animator.SetFloat("pushSpeed", 1 + GameManager.speed * 3f);
            }
        }

    }

}
