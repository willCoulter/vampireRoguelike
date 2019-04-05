using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int itemsGathered;
    public int enemiesSlain;
    public Time timeSurvived;
    public Dictionary<int, bool> roomData;

    public LevelData()
    {
        levelNumber = GameManager.instance.currentLevelNum;
        itemsGathered = GameManager.instance.itemsGathered;
        enemiesSlain = GameManager.instance.enemiesSlain;
        timeSurvived = GameManager.instance.timePlayed;

        foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            Room room = roomObject.GetComponent<Room>();

            if (room != null)
            {
                roomData.Add(room.roomID, room.isCleared);
            }
        }
    }
}
