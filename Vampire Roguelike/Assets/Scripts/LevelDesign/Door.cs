using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject door;
    BoxCollider2D doorCollider;

    bool isLocked;

    private void Start()
    {
        //If there is an attached door, grab box collider
        if(door != null){
            doorCollider = door.GetComponent<BoxCollider2D>();
        }
    }

    private void Update()
    {
        //If door locked, disable collider
        if(isLocked){
            doorCollider.enabled = false;
        //If not locked and not enabled, enable collider
        }else if(doorCollider.enabled == false && isLocked == false){
            doorCollider.enabled = true;
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
