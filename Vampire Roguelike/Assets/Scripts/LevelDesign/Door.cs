using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D doorCollider;
    SpriteRenderer sr;
    bool isLocked;
    public bool isHorizontal;

    public Sprite horizontalClosed;
    public Sprite horizontalOpen;

    private void Start()
    {
        isLocked = false;

        //If there is an attached door, grab box collider
        if(gameObject != null){
            doorCollider = gameObject.GetComponent<BoxCollider2D>();
            sr = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        //If door locked, enable collider
        if(isLocked){
            doorCollider.enabled = true;

            //If horizontal door, change sprite
            if (isHorizontal)
            {
                sr.sprite = horizontalClosed;
            }

        //If not locked and enabled, disable collider
        }else if(doorCollider.enabled == true && isLocked == false){
            doorCollider.enabled = false;

            //If horizontal door, change sprite
            if (isHorizontal)
            {
                sr.sprite = horizontalOpen;
            }

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
