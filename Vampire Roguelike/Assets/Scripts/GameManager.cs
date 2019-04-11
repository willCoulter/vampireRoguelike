using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject managerRoot;
    public static GameManager instance;
    public GameObject player;

    private Dictionary<string, KeyCode> Controls = new Dictionary<string, KeyCode>();

    //Keep track of enemies slain
    public int enemiesSlain;

    //Keep track of time played
    public int timePlayed;

    //Keep track of level
    public int currentLevelNum;

    public int itemsGathered;

    void Awake()
    {
        instance = this;

        Time.timeScale = 1;

        currentLevelNum = 1;
    }

    public void ReturnToMainMenu()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToMainMenuNoSave()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        switch (currentLevelNum)
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                break;
            default:
                Debug.Log("Invalid level number");
                break;
        }
    }
}
