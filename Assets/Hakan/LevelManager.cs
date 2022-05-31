using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static List<Level> levelDatas = new List<Level>();
    public List<Transform> characterPositions;
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
        for (int i = 0; i < GameManager.teamLayout.Count; i++)
        {
            if (GameManager.teamLayout[i] != 0)
            {
                GameManager.currentTeam.Add(PoolManager.instance.characters[PoolManager.instance.characters.Count - 1]);
                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.SetParent(characterPositions[i]);
                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.localPosition = new Vector3(0, 0, 0);

                GameManager.currentTeam[GameManager.currentTeam.Count - 1].transform.gameObject.SetActive(true);
            }
        }
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


