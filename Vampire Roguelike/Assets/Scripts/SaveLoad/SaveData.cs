using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public LevelData levelData;
    public CameraData cameraData;
    public PlayerData playerData;

    public SaveData(LevelData level, CameraData camera, PlayerData player)
    {
        levelData = level;
        cameraData = camera;
        playerData = player;
    }
}
