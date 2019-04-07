using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    //Keep track of enemies slain
    public int enemiesSlain;

    //Keep track of time played
    public int timePlayed;

    //Keep track of level
    public int currentLevelNum;

    public int itemsGathered;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        currentLevelNum = 1;
    }
}
