using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public LevelData levelData;
    public PlayerData playerData;

    public SaveData(LevelData level, PlayerData player)
    {
        levelData = level;
        playerData = player;
    }
}
