using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    void Awake()
    {
        instance = this;
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

        switch (saveData.levelData.levelNumber)
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                SceneManager.sceneLoaded += OnSceneLoaded;
                break;
            default:
                break;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveData saveData = SaveSystem.LoadGame();

        //Set player values and position
        PlayerController.instance.health = saveData.playerData.currentHealth;
        PlayerController.instance.blood = saveData.playerData.currentBlood;
        PlayerController.instance.gold = saveData.playerData.currentGold;
        PlayerController.instance.gameObject.transform.position = new Vector3(saveData.playerData.position[0], saveData.playerData.position[1], saveData.playerData.position[2]);

        //Set player skills
        int index = 0;

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
                    index++;
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
        int i = 0;
        foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            Room room = roomObject.GetComponent<Room>();

            if (room != null)
            {
                if (saveData.levelData.roomData.ContainsKey(room.roomID))
                {
                    room.isCleared = saveData.levelData.roomData[i];
                }
            }

            i++;
        }

        //Set gamemanager values
        GameManager.instance.currentLevelNum = saveData.levelData.levelNumber;
        GameManager.instance.enemiesSlain = saveData.levelData.enemiesSlain;
        GameManager.instance.timePlayed = saveData.levelData.timeSurvived;
        GameManager.instance.itemsGathered = saveData.levelData.itemsGathered;
    }

}
