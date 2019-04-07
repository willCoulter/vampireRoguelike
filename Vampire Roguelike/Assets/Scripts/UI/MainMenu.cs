using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject newGameWarning;

    public void NewGame()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            newGameWarning.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void NewGameConfirm()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGameDecline()
    {
        newGameWarning.SetActive(false);
    }

    public void ContinueGame()
    {
        SaveManager.instance.LoadGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
