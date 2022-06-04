using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Coroutine coroutine;
    private int unitCounter = 0;
    private int teamLayoutIndex = 0;
    private int characterPrice = 0;
    public void btn_StartClick()
    {
        GameManager.isGameStart = true;
        GameManager.SavePref_TeamLayout();
        UiManager.instance.powerUpImage.fillAmount = 0;
        GameManager.tapPower = 0;
        UiManager.instance.StartPanel.SetActive(false);
        UiManager.instance.gameScreenPanel.SetActive(true);
        PushMovement.SetSpeed();

        foreach (var character in GameManager.currentTeam)
        {
            character.ChangeAnimation(isGameStart: true);
            character.SetDustParticle(true);
            if (character.gameObject.activeSelf)
                character.SetCrackParticle(true);
        }
        foreach (var enemy in GameManager.enemyTeam)
        {
            enemy.ChangeAnimation(isGameStart: true);
            enemy.SetDustParticle(true);
        }
    }
    public void btn_BuyUnitClick()
    {
        if (GameManager.totalGold >= GameManager.priceChar)
        {
            int i = 0,lowestLevelIndex=0,lowestLevel=99,lowestCaharacterPositionsIndex;
            bool fullBool = false;
            if(GameManager.currentTeam.Count==5)
            {
                fullBool = true; ;
            }
            foreach (var characterPosition in LevelManager.instance.characterPositions)
            {
                if (GameManager.currentTeam.Count != 5)
                {
                    if (characterPosition.transform.childCount == 0)
                    {
                        GameManager.currentTeam.Add(PoolManager.instance.characters[PoolManager.instance.characters.Count - 1]);
                        PoolManager.instance.characters.RemoveAt(PoolManager.instance.characters.Count - 1);
                        GameManager.currentTeam[GameManager.currentTeam.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.SetParent(characterPosition);
                        GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);

                        GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.gameObject.SetActive(true);
                        GameManager.currentTeam[GameManager.currentTeam.Count - 1].SetSpawnParticle(true);

                        GameManager.teamLayout[teamLayoutIndex] = 1;
                        GameManager.ourPower++;
                        PushMovement.SetSpeed();
                        GameManager.totalGold -= GameManager.priceChar;
                        characterPrice = GameManager.priceChar;
                        UiManager.instance.goldText.text = GameManager.totalGold.ToString();
                        break;
                    }
                    else
                    {
                        unitCounter++;
                    }
                    teamLayoutIndex++;
                }
                else
                {
                    if(GameManager.currentTeam[i].level<=lowestLevel)
                    {
                        lowestLevelIndex = i;
                        lowestLevel = GameManager.currentTeam[i].level;
                    }
                        i++;
                }

                
            }
            for(int k=0;k<GameManager.currentTeam.Count;k++)
            {
                if (GameManager.currentTeam[k].level <= lowestLevel)
                {
                    lowestLevelIndex = k;
                    lowestLevel = GameManager.currentTeam[k].level;
                }
            }
            
            if (fullBool)
            {
                GameManager.currentTeam[lowestLevelIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                GameManager.currentTeam[lowestLevelIndex].transform.localPosition = new Vector3(0, 0, 0);
                if (lowestLevel * 2 == 2 || lowestLevel * 2 == 8)
                {
                    GameManager.currentTeam[lowestLevelIndex].particleDust.transform.localScale = (lowestLevel * 2) == 2 ? Vector3.one * 0.035f * 1.5f : Vector3.one * 0.035f * 2.0f;
                    GameManager.currentTeam[lowestLevelIndex].transform.localScale =Vector3.one+( (lowestLevel * 2) == 2 ? Vector3.one * 0.2f : Vector3.one * 0.4f);

                }
                GameManager.currentTeam[lowestLevelIndex].level = lowestLevel * 2;
                GameManager.currentTeam[lowestLevelIndex].TextMeshPro.text = (lowestLevel * 2).ToString();
                GameManager.ourPower += (lowestLevel * 2 * 2 - 1) - (lowestLevel * 2 - 1) * 2;
                GameManager.teamLayout[LevelManager.instance.characterPositions.IndexOf(GameManager.currentTeam[lowestLevelIndex].transform.parent)] = lowestLevel * 2;
                GameManager.currentTeam[lowestLevelIndex].SetLevelupParticle(true);
                if (Math.Log(lowestLevel * 2, 2) <= 6)
                {
                    for (int j = 0; j < Math.Log(lowestLevel * 2, 2); j++)
                    {
                        GameManager.currentTeam[lowestLevelIndex].outfits[j].SetActive(true);
                    }
                }
                GameManager.totalGold -= Int32.Parse(UiManager.instance.unitBuyGoldText.text);
                UiManager.instance.goldText.text = GameManager.totalGold.ToString();

                /* UiManager.instance.fullText.gameObject.SetActive(true);
                 if (coroutine != null)
                     StopCoroutine(coroutine);
                 coroutine = StartCoroutine(UiManager.instance.fullText.GetComponent<TextAnimation>().ScaleAnimation());*/

            }
            lowestLevel = 99;
            for (int k = 0; k < GameManager.currentTeam.Count; k++)
            {
                if (GameManager.currentTeam[k].level <= lowestLevel)
                {
                    lowestLevelIndex = k;
                    lowestLevel = GameManager.currentTeam[k].level;
                }
            }
            if (GameManager.currentTeam.Count == 5)
            {
                UiManager.instance.unitBuyGoldText.text = (lowestLevel * characterPrice).ToString();
            }
            else
            {
                UiManager.instance.unitBuyGoldText.text = characterPrice .ToString();
            }
            unitCounter = 0;
            teamLayoutIndex = 0;
        }


    }

    public void btn_NextLevelClick()
    {
        PoolManager.instance.ResetPool();
        UiManager.instance.fx_WinConfetti.SetActive(false);
        UiManager.instance.powerUpImage.fillAmount = 0;
        GameManager.tapPower = 0;

        GameManager.SavePref_TeamLayout();

        GameManager.levelNumber++;
        PlayerPrefs.SetInt("LevelNumber", GameManager.levelNumber);

        if (GameManager.levelNumber > GameManager.totalLevelCount)
        {
            GameManager.levelNumber = UnityEngine.Random.Range(5, GameManager.totalLevelCount);
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
        UiManager.instance.powerUpImage.fillAmount = 0;
        GameManager.tapPower = 0;
        GameManager.SavePref_TeamLayout();
        GameManager.ResetDefaults();
        GameManager.totalGold += GameManager.loseGold;
        PlayerPrefs.SetInt("Gold", GameManager.totalGold);
        PlayerPrefs.Save();

        UiManager.instance.loseScreenPanel.SetActive(false);

        LevelManager.instance.CreateLevel();

        UiManager.instance.StartPanel.SetActive(true);

    }



}
