using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapToPush : MonoBehaviour
{

    float timeLapse = 0.0f;
    float totalTime = 3f;

    public int power = 0;

    private void FixedUpdate()
    {
        if (GameManager.isGameStart == true && GameManager.isGameEnd == false)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                timeLapse += 0.4f;
                if (UiManager.instance.powerUpImage.fillAmount >= 1)
                    UiManager.instance.powerUpImage.fillAmount = 1;
            }
            if (UiManager.instance.powerUpImage.fillAmount <= 0)
            {
                UiManager.instance.powerUpImage.fillAmount = 0;
            }
            timeLapse -= Time.deltaTime / 4;
            timeLapse = Mathf.Clamp(timeLapse, 0.0f, totalTime);

            UiManager.instance.powerUpImage.fillAmount = timeLapse / totalTime;
        }

        if (UiManager.instance.powerUpImage.fillAmount > 0)
        {
            GameManager.tapPower = 1;
            PushMovement.SetSpeed();
        }
        else
        {
            GameManager.tapPower = 0;
            PushMovement.SetSpeed();
        }
    }
}
