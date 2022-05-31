using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public Character characterPose;
    public List<Character> characters = new List<Character>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void CreatePool()
    {        
        for (int i = 0; i < 10; i++)
        {
            Character tempCharacter = Instantiate(characterPose, this.transform);
            tempCharacter.gameObject.SetActive(false);
            characters.Add(tempCharacter);
        }
    }

}
