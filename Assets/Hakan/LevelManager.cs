using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static List<Level> levelDatas = new List<Level>();
    public List<Transform> characterPositions;
    public List<Transform> enemyPositions;

    public GameObject mainParent;

    private Vector3 mainParentDefaultTransform = new Vector3(0f, 0f, 7f);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void LoadLevels()
    {
        for (int i = 0; i < Resources.LoadAll<TextAsset>("Levels").Length; i++)
        {
            TextAsset jsonInfo = Resources.Load<TextAsset>("Levels/Level" + i);
            levelDatas.Add(JsonUtility.FromJson<Level>(jsonInfo.text));
        }
    }

    public void CreateLevel()
    {
        SetLevelVariables();
        SetTextValues();
        mainParent.transform.position = mainParentDefaultTransform;

        for (int i = 0; i < GameManager.teamLayout.Count; i++)
        {
            if (GameManager.teamLayout[i] != 0)
            {
                GameManager.currentTeam.Add(PoolManager.instance.characters[PoolManager.instance.characters.Count - 1]);
                //var tempPoolCharacter = PoolManager.instance.characters.Find(x => x.level == GameManager.teamLayout[i]);
                //GameManager.currentTeam.Add(tempPoolCharacter);

                Character tempCharacter = GameManager.currentTeam[GameManager.currentTeam.Count - 1];
                tempCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                PoolManager.instance.characters.RemoveAt(PoolManager.instance.characters.Count - 1);
                //PoolManager.instance.characters.Remove(tempPoolCharacter);
                tempCharacter.transform.SetParent(characterPositions[i]);
                tempCharacter.transform.localPosition = new Vector3(0, 0, 0) + tempCharacter.level * Vector3.up * 0.01f; 
                tempCharacter.transform.gameObject.SetActive(true);

                tempCharacter.level = GameManager.teamLayout[i];
                tempCharacter.TextMeshPro.text = tempCharacter.level.ToString();

                if (Math.Log(tempCharacter.level, 2) <= 6 && tempCharacter.level != 1)
                {
                    for (int j = 0; j < Math.Log(tempCharacter.level, 2); j++)
                    {
                        tempCharacter.outfits[j].SetActive(true);
                    }
                }
                if (tempCharacter.level == 2 || tempCharacter.level == 4)
                {
                    tempCharacter.transform.localScale = new Vector3((float)(transform.localScale.x + 0.2), (float)(transform.localScale.y + 0.2),
                      (float)(transform.localScale.z + 0.2));

                    tempCharacter.transform.localPosition = new Vector3(0, 0, 0) + tempCharacter.level * Vector3.up * 0.01f;
                    Transform tempParticleDust = tempCharacter.transform.GetComponent<Character>().particleDust.transform;

                    tempCharacter.transform.GetComponent<Character>().particleDust.transform.localScale = Vector3.one * 0.035f*1.5f; //new Vector3(tempParticleDust.localScale.x / 5,tempParticleDust.localScale.y / 5, tempParticleDust.localScale.z / 5);
                }
                if (tempCharacter.level >= 8)
                {
                    tempCharacter.transform.localScale = new Vector3((float)(transform.localScale.x + 0.2 * 2), (float)(transform.localScale.y + 0.2 * 2),
                     (float)(transform.localScale.z + 0.2 * 2));

                    tempCharacter.transform.localPosition = new Vector3(0, 0, 0) + tempCharacter.level * Vector3.up * 0.01f;
                    Transform tempParticleDust = tempCharacter.transform.GetComponent<Character>().particleDust.transform;

                    tempCharacter.transform.GetComponent<Character>().particleDust.transform.localScale = Vector3.one * 0.035f * 2f;// new Vector3(tempParticleDust.localScale.x * 0.5f * 2,tempParticleDust.localScale.y * 0.5f * 2, tempParticleDust.localScale.z * 0.5f * 2);

                }
                GameManager.ourPower += GameManager.teamLayout[i]*2-1;
            }
        }
        foreach (var enemyData in levelDatas[GameManager.levelNumber].enemiesData)
        {
            GameManager.enemyTeam.Add(PoolManager.instance.enemies[PoolManager.instance.enemies.Count - 1]);

            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            PoolManager.instance.enemies.RemoveAt(PoolManager.instance.enemies.Count - 1);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.SetParent(enemyPositions[enemyData.position - 1]);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].GetComponentInChildren<TextMeshPro>().text = enemyData.level.ToString();
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.gameObject.SetActive(true);

            GameManager.enemyPower += enemyData.level*2-1;
            GameManager.enemyLayout[enemyData.position - 1] = enemyData.level;
        }
        PushMovement.SetSpeed();
    }

    public void SetLevelVariables()

    {
        GameManager.loseGold = levelDatas[GameManager.levelNumber].loseGold;
        GameManager.priceChar = levelDatas[GameManager.levelNumber].priceChar;
        GameManager.winGold = levelDatas[GameManager.levelNumber].winGold;
    }
    public void SetTextValues()
    {

        UiManager.instance.levelText.text = "Level " + (GameManager.levelNumber + 1).ToString();
        UiManager.instance.goldText.text = GameManager.totalGold.ToString();
        UiManager.instance.winGoldText.text = GameManager.winGold.ToString();
        UiManager.instance.loseGoldText.text = GameManager.loseGold.ToString();
        UiManager.instance.unitBuyGoldText.text = GameManager.priceChar.ToString();
    }
}


[System.Serializable]
public class Level
{
    public int winGold;
    public int loseGold;
    public int priceChar;
    [SerializeField] public EnemiesData[] enemiesData;
}

[System.Serializable]
public class EnemiesData
{
    public int position;
    public int level;
}


