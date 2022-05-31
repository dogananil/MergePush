using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMovement : MonoBehaviour
{

    private void Start()
    {
        GameManager.speed = ((float)GameManager.ourPower - (float)GameManager.enemyPower) / 10 + GameManager.speed;
        GameManager.speed = Mathf.Clamp(GameManager.speed, -1, 1);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * GameManager.speed);
        Debug.Log(GameManager.speed);
    }

    public static void SetSpeed()
    {
        GameManager.speed = ((float)GameManager.ourPower - (float)GameManager.enemyPower) / 10;
        GameManager.speed = Mathf.Clamp(GameManager.speed, -1, 1);
        Debug.Log("speed: " + GameManager.speed);
    }

}
