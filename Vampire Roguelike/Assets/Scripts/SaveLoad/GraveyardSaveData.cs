using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraveyardSaveData
{
    public LevelData levelData;
    public PlayerData playerData;
    public System.DateTime finalDeathTime;

    public GraveyardSaveData(LevelData level, PlayerData player, System.DateTime timeStamp)
    {
        levelData = level;
        playerData = player;
        finalDeathTime = timeStamp;
    }
}