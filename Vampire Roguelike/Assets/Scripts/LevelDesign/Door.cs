using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D doorCollider;

    bool isLocked;

    private void Start()
    {
        isLocked = false;

        //If there is an attached door, grab box collider
        if(gameObject != null){
            doorCollider = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    private void Update()
    {
        //If door locked, enable collider
        if(isLocked){
            doorCollider.enabled = true;
        //If not locked and enabled, disable collider
        }else if(doorCollider.enabled == true && isLocked == false){
            doorCollider.enabled = false;
        }
    }

    public void LockDoor(){
        isLocked = true;
    }

    public void UnLockDoor()
    {
        isLocked = false;
    }
}
