using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public List<GameObject> roomsRemaining;

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
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
        Time.timeScale = 1;

        currentLevelNum = 1;
    }

    void Update()
    {
        CountRooms();
    }

    public void CountRooms()
    {
        //Grab all room objects and reset rooms remaining
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        roomsRemaining = new List<GameObject>();

        //Loop through rooms
        foreach(GameObject room in rooms)
        {
            //If room is not cleared, add to rooms remaining
            if (!room.GetComponent<Room>().isCleared)
            {
                roomsRemaining.Add(room);
            }
        }

    }

    public void NextFloor()
    {
        switch (currentLevelNum)
        {
            case 1:
                SceneManager.LoadScene("Level2");
                break;
            case 2:
                SceneManager.LoadScene("Level3");
                break;
            case 3:
                SceneManager.LoadScene("Level4");
                break;
            case 4:
                SceneManager.LoadScene("Level5");
                break;
            default:
                break;
        }

        UIManager.instance.RefreshSkillSlots();
    }

    public void ReturnToMainMenu()
    {
        SaveManager.instance.SaveGame();
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToMainMenuNoSave()
    {
        Destroy(AudioManager.instance.gameObject);
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
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                break;
            case 4:
                SceneManager.LoadScene("Level4");
                break;
            case 5:
                SceneManager.LoadScene("Level5");
                break;
            default:
                break;
        }
    }
}
