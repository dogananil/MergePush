using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameStart = false;
    public static int ourPower = 4;
    public static int enemyPower = 5;
    public static float speed = 0;
    public static int levelNumber = 0;
    public static int totalGold = 0;

    public static List<int> teamLayout = new List<int>();

    public static List<Character> currentTeam = new List<Character>();
    public static List<Character> enemyTeam = new List<Character>();

    private void Start()
    {
        PoolManager.instance.CreatePool();
        LoadPrefs();
        LevelManager.instance.LoadLevels();
        LevelManager.instance.CreateLevel();
    }
    private void LoadPrefs()
    {
        totalGold = PlayerPrefs.GetInt("Gold", 0);
        var tempList =PlayerPrefs.GetString("TeamLayout", "0-0-1-0-0").Split('-');
        levelNumber = PlayerPrefs.GetInt("LevelNumber", 0);
        foreach(var element in tempList)
        {
             teamLayout.Add(int.Parse(element));
        }
    }
}
