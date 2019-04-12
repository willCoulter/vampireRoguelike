using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public List<GameObject> roomsRemaining;

    private Dictionary<string, KeyCode> Controls = new Dictionary<string, KeyCode>();

    //Keep track of enemies slain
    public int enemiesSlain;

    //Keep track of time played
    public float timePlayed = 0.0f;
    public TimeSpan timePlayedFormatted;

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

        //If not on main menu, start timer
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            timePlayed += Time.deltaTime;
            int seconds = Convert.ToInt32(timePlayed % 60);
            timePlayedFormatted = TimeSpan.FromSeconds(seconds);
        }
    }

    public string GetTimerString()
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timePlayedFormatted.Hours, timePlayedFormatted.Minutes, timePlayedFormatted.Seconds);
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

        instance.itemsGathered = 0;
        UIManager.instance.RefreshSkillSlots();
    }

    public void ReturnToMainMenu()
    {
        SaveManager.instance.SaveGame();
        Destroy(AudioManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToMainMenuNoSave()
    {
        Destroy(AudioManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        enemiesSlain = 0;
        itemsGathered = 0;

        SkillInventory.instance.skills[0] = null;
        SkillInventory.instance.skills[1] = null;
        SkillInventory.instance.skills[2] = null;

        ItemInventory.instance.ClearAllItems();

        PlayerController.instance.ResetToDefaults();

        timePlayed = 0.0f;

        Time.timeScale = 1;

        AudioManager.instance.audioSource.Play();

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

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraMovement cameraScript = camera.GetComponent<CameraMovement>();

        cameraScript.followTarget = PlayerController.instance.gameObject;
    }
}
