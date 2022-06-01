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

                GameManager.currentTeam[GameManager.currentTeam.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                PoolManager.instance.characters.RemoveAt(PoolManager.instance.characters.Count - 1);
                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.SetParent(characterPositions[i]);
                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);
                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.gameObject.SetActive(true);

                GameManager.ourPower += GameManager.teamLayout[i];
            }
        }
        foreach (var enemyData in levelDatas[GameManager.levelNumber].enemiesData)
        {
            GameManager.enemyTeam.Add(PoolManager.instance.enemies[PoolManager.instance.enemies.Count - 1]);

            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            PoolManager.instance.enemies.RemoveAt(PoolManager.instance.enemies.Count - 1);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.SetParent(enemyPositions[enemyData.position - 1]);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].GetComponentInChildren<TextMeshPro>().text = enemyData.level.ToString();
            GameManager.enemyTeam[GameManager.enemyTeam.Count - 1].transform.gameObject.SetActive(true);

            GameManager.enemyPower += enemyData.level;
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

        UiManager.instance.levelText.text = "Level " + GameManager.levelNumber.ToString();
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


