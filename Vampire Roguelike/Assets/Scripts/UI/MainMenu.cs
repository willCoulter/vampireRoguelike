using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject newGameWarning;
    public GameObject creditsPage;
    public GameObject buttonsWrapper;
    public GameObject continueButton;

    void Update()
    {
        ToggleContinueButton();
    }

    public void NewGame()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            newGameWarning.SetActive(true);
        }
        else
        {
            NewGameConfirm();
        }
    }

    public void NewGameConfirm()
    {
        SaveManager.instance.isLoading = false;
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

    public void ShowCredits()
    {
        buttonsWrapper.SetActive(false);
        creditsPage.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPage.SetActive(false);
        buttonsWrapper.SetActive(true);
    }

    public void ToggleContinueButton()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
}
