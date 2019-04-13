using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[System.Serializable]
public static class SaveSystem
{
    public static void SaveGame(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);
        LevelData levelData = new LevelData(GameManager.instance);

        SaveData saveData = new SaveData(levelData, playerData);

        formatter.Serialize(stream, saveData);

        stream.Close();
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveToGraveyard() { 
    
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/save.dat";

        //If save file exists
        if (File.Exists(savePath))
        {
            //Load the save and store in variable
            SaveData save = LoadGame();

            var currentTime = System.DateTime.Now;

            GraveyardSaveData graveyardSave = new GraveyardSaveData(save.levelData, save.playerData, currentTime);

            //Create new file on graveyard path with current timestamp
            string graveyardPath = Application.persistentDataPath + "/Graveyard" + "/save-" + currentTime.ToString("dd-MM-yyyy_hh-mm-ss") + ".dat";
            FileStream stream = new FileStream(graveyardPath, FileMode.Create);

            //Serialize data into graveyard save
            formatter.Serialize(stream, graveyardSave);
            stream.Close();

            //Delete save
            File.Delete(savePath);
        }
        else
        {
            Debug.Log("No save data present");
        }
    }

    public static List<GraveyardSaveData> LoadGraveyardSaves()
    {
        List<GraveyardSaveData> saves = new List<GraveyardSaveData>();

        //Load directory of graveyard files
        string graveyardPath = Application.persistentDataPath + "/graveyard";
        DirectoryInfo dir = new DirectoryInfo(graveyardPath);

        //Grab all .dat save files
        FileInfo[] info = dir.GetFiles("*.dat");

        BinaryFormatter formatter = new BinaryFormatter();

        //Iterate through files
        foreach (FileInfo save in info)
        {
            //Get save path and create filestream
            string savePath = save.FullName;
            FileStream saveStream = new FileStream(savePath, FileMode.Open);

            //Deserialize data and add to list
            saves.Add(formatter.Deserialize(saveStream) as GraveyardSaveData);

            saveStream.Close();
        }

        return saves;
    }
}
