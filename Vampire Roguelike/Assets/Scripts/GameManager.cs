using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject managerRoot;
    public static GameManager instance;
    public GameObject player;

    private Dictionary<string, KeyCode> Controls = new Dictionary<string, KeyCode>();

    public Text up, left,down,right,dodge, interact, bloodSuck, slot1,slot2,slot3;

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
        DontDestroyOnLoad(managerRoot);

        currentLevelNum = 1;
    }
}
