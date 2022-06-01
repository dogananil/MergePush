using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public Character characterPose;
    public Enemy enemyPose;
    public List<Character> characters = new List<Character>();
    public List<Enemy> enemies = new List<Enemy>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void CreatePool()
    {        
        for (int i = 0; i < 20; i++)
        {
            Character tempCharacter = Instantiate(characterPose, this.transform);
            tempCharacter.gameObject.SetActive(false);
            characters.Add(tempCharacter);
        }
        for (int i = 0; i < 5; i++)
        {
            Enemy tempEnemy = Instantiate(enemyPose, this.transform);
            tempEnemy.gameObject.SetActive(false);
            enemies.Add(tempEnemy);
        }
    }
    public void ResetPool()
    {
        foreach(var characterPosition in LevelManager.instance.characterPositions)
        {
            if (characterPosition.transform.childCount > 0)
            {
                Transform tempCharacter = characterPosition.GetChild(0);
                tempCharacter.gameObject.SetActive(false);
                tempCharacter.transform.SetParent(null);
                characters.Add(tempCharacter.GetComponent<Character>());
            }
            GameManager.currentTeam.Clear();
        }
        foreach (var enemyPosition in LevelManager.instance.enemyPositions)
        {
            if (enemyPosition.transform.childCount > 0)
            {
                Transform tempEnemy = enemyPosition.GetChild(0);
                tempEnemy.gameObject.SetActive(false);
                tempEnemy.transform.SetParent(null);
                enemies.Add(tempEnemy.GetComponent<Enemy>());
            }
            GameManager.enemyTeam.Clear();
        }
    }

}
