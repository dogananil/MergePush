using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMovement : MonoBehaviour
{

    private void Start()
    {

    }
    void FixedUpdate()
    {
        if (GameManager.isGameStart == true && GameManager.isGameEnd == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameManager.speed);
        }
    }

    public static void SetSpeed()
    {
        GameManager.speed = ((float)GameManager.ourPower - (float)GameManager.enemyPower + (float)GameManager.tapPower) /4;
        GameManager.speed = Mathf.Clamp(GameManager.speed, -1, 1);
    }

}
