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
    public Time timePlayed;

    //Keep track of level
    public int currentLevelNum;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentLevelNum = 1;
    }
}
