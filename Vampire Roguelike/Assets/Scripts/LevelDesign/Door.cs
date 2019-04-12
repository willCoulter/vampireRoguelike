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
    public AudioClip doorLockClip;


    SpriteRenderer[] srArray;

    private void Start()
    {
        isLocked = false;

        //If there is an attached door, grab box collider
        if(gameObject != null){
            doorCollider = gameObject.GetComponent<BoxCollider2D>();

            if (isHorizontal)
            {
                sr = gameObject.GetComponent<SpriteRenderer>();
            }else if (!isHorizontal)
            {
                srArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
            }
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
            else
            {
                for(int i = 0; i < srArray.Length; i++)
                {
                    srArray[i].enabled = true;
                }
            }

        //If not locked and enabled, disable collider
        }else if(doorCollider.enabled == true && isLocked == false){
            doorCollider.enabled = false;

            //If horizontal door, change sprite
            if (isHorizontal)
            {
                sr.sprite = horizontalOpen;
            }
            else
            {
                for (int i = 0; i < srArray.Length; i++)
                {
                    srArray[i].enabled = false;
                }
            }
        }
    }

    public void LockDoor(){
        AudioManager.instance.audioSource.PlayOneShot(doorLockClip);
        isLocked = true;
    }

    public void UnLockDoor()
    {
        isLocked = false;
    }
}
