using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stairs : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        //If player is in range and presses interact button
        if(playerInRange && Input.GetKeyDown(PlayerController.instance.playerControls["Interact"]))
        {
            GameManager.instance.NextFloor();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShowConfirmPopup();
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        HideConfirmPopup();
        playerInRange = false;
    }

    void ShowConfirmPopup()
    {
        UIManager.instance.ShowConfirmPopup(transform.position);
    }

    void HideConfirmPopup()
    {
        UIManager.instance.HideConfirmPopup();
    }
}
