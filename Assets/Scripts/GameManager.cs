using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Facebook.Unity;

public class GameManager : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameEnd = false;
    public static int ourPower = 0;
    public static int enemyPower = 0;
    public static int tapPower = 0;
    public static float speed = 0;
    public static int levelNumber = 0;
    public static int totalGold = 0;
    public static int totalLevelCount = 0;
    public static int winGold;
    public static int loseGold;
    public static int priceChar;



    public static List<int> teamLayout = new List<int>();
    public static List<int> enemyLayout = new List<int>() { 0, 0, 0, 0, 0 };

    public static List<Character> currentTeam = new List<Character>();
    public static List<Enemy> enemyTeam = new List<Enemy>();

    private void Start()
    {
        /* PlayerPrefs.SetInt("Gold", 0);
         PlayerPrefs.SetString("TeamLayout", "0-0-1-0-0");
         PlayerPrefs.SetInt("LevelNumber", 0);
         PlayerPrefs.Save();*/
        FB.Init("570308681379162");

        PoolManager.instance.CreatePool();
        LoadPrefs();
        LevelManager.instance.LoadLevels();
        totalLevelCount = LevelManager.levelDatas.Count;
        LevelManager.instance.CreateLevel();
    }
    private void LoadPrefs()
    {
        totalGold = PlayerPrefs.GetInt("Gold", 100);
        var tempList = PlayerPrefs.GetString("TeamLayout", "0-0-1-0-0").Split('-');
        levelNumber = PlayerPrefs.GetInt("LevelNumber", 0);
        foreach (var element in tempList)
        {
            teamLayout.Add(int.Parse(element));
        }
    }
    public void SavePrefs()
    {
        PlayerPrefs.SetInt("Gold", totalGold);
        PlayerPrefs.SetInt("LevelNumber", levelNumber);

        List<string> tempList = new List<string>();
        foreach (var element in teamLayout)
        {
            tempList.Add(element.ToString());
            tempList.Add("-");
        }
        tempList.RemoveAt(tempList.Count - 1);
        string tempTeamLayout = "";

        foreach (var element in tempList)
        {
            tempTeamLayout += element;
        }

        PlayerPrefs.SetString("TeamLayout", tempTeamLayout);
        PlayerPrefs.Save();
    }

    public static void SavePref_TeamLayout()
    {
        List<string> tempList = new List<string>();
        foreach (var element in teamLayout)
        {
            tempList.Add(element.ToString());
            tempList.Add("-");
        }
        tempList.RemoveAt(tempList.Count - 1);
        string tempTeamLayout = "";

        foreach (var element in tempList)
        {
            tempTeamLayout += element;
        }

        PlayerPrefs.SetString("TeamLayout", tempTeamLayout);
        PlayerPrefs.Save();
    }

    public void ChangeLevel()
    {

    }
    public static void ResetDefaults()
    {
        isGameStart = false;
        isGameEnd = false;
        ourPower = 0;
        enemyPower = 0;
        speed = 0;
    }
}
