using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int itemsGathered;
    public int enemiesSlain;
    public float timeSurvived;
    public bool bossDefeated;
    public Dictionary<int, bool> roomData;

    public LevelData(GameManager instance)
    {
        roomData = new Dictionary<int, bool>();

        levelNumber = instance.currentLevelNum;
        itemsGathered = instance.itemsGathered;
        enemiesSlain = instance.enemiesSlain;
        timeSurvived = instance.timePlayed;
        bossDefeated = false;

        foreach (GameObject roomObject in GameObject.FindGameObjectsWithTag("Room"))
        {
            Room room = roomObject.GetComponent<Room>();

            if (room != null)
            {
                roomData.Add(room.roomID, room.isCleared);

                //If they cleared the boss room, boss is defeated
                if(room.isBossRoom && room.isCleared)
                {
                    bossDefeated = true;
                }
            }
        }
    }
}
