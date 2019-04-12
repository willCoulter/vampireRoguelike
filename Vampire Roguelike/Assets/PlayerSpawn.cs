using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn instance;

    void Awake()
    {
        instance = this;

        if(PlayerController.instance != null)
        {
            MovePlayerToSpawn();
        }
    }
    
    public void MovePlayerToSpawn()
    {
        PlayerController.instance.gameObject.transform.position = transform.position;
    }
}
