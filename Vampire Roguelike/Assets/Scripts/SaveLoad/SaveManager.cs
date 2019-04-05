using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public CameraMovement camera;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one SaveManager instance");
            return;
        }

        instance = this;
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(camera, PlayerController.instance);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();

        camera.followTarget = saveData.cameraData.cameraTrackTarget;
        camera.gameObject.GetComponent<Camera>().depth = saveData.cameraData.cameraZDepth;
        camera.moveSpeed = saveData.cameraData.moveSpeed;

        PlayerController.instance.health = saveData.playerData.currentHealth;
        PlayerController.instance.blood = saveData.playerData.currentBlood;
        PlayerController.instance.gold = saveData.playerData.currentGold;
        PlayerController.instance.gameObject.transform.position = new Vector3(saveData.playerData.position[0], saveData.playerData.position[1], saveData.playerData.position[2]);

        int i = 0;

        foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            Room room = roomObject.GetComponent<Room>();

            if (room != null)
            {
                if(saveData.levelData.roomData.ContainsKey(room.roomID))
                {
                    room.isCleared = saveData.levelData.roomData[i];
                }
            }

            i++;
        }

        GameManager.instance.currentLevelNum = saveData.levelData.levelNumber;
        GameManager.instance.enemiesSlain = saveData.levelData.enemiesSlain;
        GameManager.instance.timePlayed = saveData.levelData.timeSurvived;
        GameManager.instance.itemsGathered = saveData.levelData.itemsGathered;

    }
}
