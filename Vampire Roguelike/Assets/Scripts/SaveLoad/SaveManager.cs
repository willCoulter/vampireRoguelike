using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public bool isLoading;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        string graveyardPath = Application.persistentDataPath + "/Graveyard";

        if (!Directory.Exists(graveyardPath)){
            Debug.Log("Created graveyard directory");
            Directory.CreateDirectory(graveyardPath);
        }
    }

    public void SaveToGraveyard()
    {
        SaveSystem.SaveToGraveyard();
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(PlayerController.instance);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();
        isLoading = true;

        switch (saveData.levelData.levelNumber)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            case 4:
                SceneManager.LoadScene("Level4");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            case 5:
                SceneManager.LoadScene("Level5");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            default:
                break;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "MainMenu" && isLoading == true)
        {
            SaveData saveData = SaveSystem.LoadGame();

            //Set player values and position
            PlayerController.instance.health = saveData.playerData.currentHealth;
            PlayerController.instance.blood = saveData.playerData.currentBlood;
            PlayerController.instance.gold = saveData.playerData.currentGold;
            PlayerSpawn.instance.gameObject.transform.position = new Vector3(saveData.playerData.position[0], saveData.playerData.position[1], saveData.playerData.position[2]);
            PlayerSpawn.instance.MovePlayerToSpawn();

            //Set store skills to null if already purchased
            if (saveData.levelData.storeSkillsPurchased[0] == true)
            {
                Destroy(Store.instance.storeSkill1);
            }

            if (saveData.levelData.storeSkillsPurchased[1] == true)
            {
                Destroy(Store.instance.storeSkill2);
            }

            //Set player skills

            //Loop through saved skill id's
            foreach (int playerSkill in saveData.playerData.skills)
            {
                //Loop through all skills
                foreach (Skill skill in SkillInventory.instance.allSkills)
                {
                    //Check if saved skill id matches a skill in all skills list
                    if (playerSkill == skill.skillID)
                    {
                        //If so, add to players current skills
                        SkillInventory.instance.Add(skill);
                    }
                }
            }

            //Set player items
            foreach (int playerItem in saveData.playerData.items)
            {
                foreach (Item item in ItemInventory.instance.allItems)
                {
                    if (playerItem == item.itemID)
                    {
                        ItemInventory.instance.items.Add(item);
                    }
                }
            }

            //Loop through room objects and set cleared value by room id
            foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
            {
                Room room = roomObject.GetComponent<Room>();

                if (room != null)
                {
                    foreach (KeyValuePair<int, bool> entry in saveData.levelData.roomData)
                    {
                        if(entry.Key == room.roomID)
                        {
                            room.isCleared = entry.Value;

                            if(saveData.levelData.bossDefeated && room.isBossRoom)
                            {
                                room.SpawnStairs();
                            }
                        }
                    }
                }
            }

            //Set gamemanager values
            GameManager.instance.currentLevelNum = saveData.levelData.levelNumber;
            GameManager.instance.enemiesSlain = saveData.levelData.enemiesSlain;
            GameManager.instance.timePlayed = saveData.levelData.timeSurvived;
            GameManager.instance.itemsGathered = saveData.levelData.itemsGathered;

            //Loading done
            isLoading = false;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
