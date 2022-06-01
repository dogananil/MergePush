using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Coroutine coroutine;
    private int unitCounter = 0;
    private int teamLayoutIndex = 0;
    public void btn_StartClick()
    {
        GameManager.isGameStart = true;
        UiManager.instance.powerUpImage.fillAmount = 0;
        UiManager.instance.StartPanel.SetActive(false);
        UiManager.instance.gameScreenPanel.SetActive(true);
        PushMovement.SetSpeed();

        foreach (var character in GameManager.currentTeam)
        {
            character.ChangeAnimation(isGameStart: true);
            character.SetDustParticle(true);
        }
        foreach (var enemy in GameManager.enemyTeam)
        {
            enemy.ChangeAnimation(isGameStart: true);
            enemy.SetDustParticle(true);
        }
    }
    public void btn_BuyUnitClick()
    {
        if(GameManager.totalGold>=GameManager.priceChar)
        {
            foreach (var characterPosition in LevelManager.instance.characterPositions)
            {
                if (characterPosition.transform.childCount == 0)
                {
                    GameManager.currentTeam.Add(PoolManager.instance.characters[PoolManager.instance.characters.Count - 1]);
                    PoolManager.instance.characters.RemoveAt(PoolManager.instance.characters.Count - 1);
                    GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.SetParent(characterPosition);
                    GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);

                    GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.gameObject.SetActive(true);

                    GameManager.teamLayout[teamLayoutIndex] = 1;
                    GameManager.ourPower++;
                    PushMovement.SetSpeed();
                    GameManager.totalGold -= GameManager.priceChar;
                    UiManager.instance.goldText.text = GameManager.totalGold.ToString();
                    break;
                }
                else
                {
                    unitCounter++;
                }
                teamLayoutIndex++;
            }
            if (unitCounter == 5)
            {
                UiManager.instance.fullText.gameObject.SetActive(true);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(UiManager.instance.fullText.GetComponent<TextAnimation>().ScaleAnimation());
            }
            unitCounter = 0;
            teamLayoutIndex = 0;
        }
        
        
    }

    public void btn_NextLevelClick()
    {
        PoolManager.instance.ResetPool();
        GameManager.levelNumber++;
        if(GameManager.levelNumber>GameManager.totalLevelCount)
        {
            GameManager.levelNumber = Random.Range(5, GameManager.totalLevelCount);
        }
        GameManager.ResetDefaults();
        GameManager.totalGold += GameManager.winGold;
        PlayerPrefs.SetInt("Gold", GameManager.totalGold);
        PlayerPrefs.Save();

        UiManager.instance.winScreenPanel.SetActive(false);

        LevelManager.instance.CreateLevel();

        UiManager.instance.StartPanel.SetActive(true);
    }
    public void btn_RestartLevelClick()
    {
        PoolManager.instance.ResetPool();

        GameManager.ResetDefaults();
        GameManager.totalGold += GameManager.loseGold;
        PlayerPrefs.SetInt("Gold", GameManager.totalGold);
        PlayerPrefs.Save();

        UiManager.instance.loseScreenPanel.SetActive(false);

        LevelManager.instance.CreateLevel();

        UiManager.instance.StartPanel.SetActive(true);
        
    }



}
