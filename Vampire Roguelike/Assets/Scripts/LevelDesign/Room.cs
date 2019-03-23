using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Keep track of how many chests dropped on this floor
    //Needed to gaurantee an item on last few rooms if none have dropped yet
    static int chestsDropped;

    public List<GameObject> doors;
    List<Door> doorScripts;

    List<Enemy> enemies;
    List<GameObject> spawnPoints;

    Transform chestLocation;

    bool isCleared;
    bool enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        //Loop through doors in array and grab script for method calling
        foreach(GameObject door in doors){
            Debug.Log(door);
            doorScripts.Add(door.GetComponent<Door>());
            Debug.Log(doorScripts);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If all enemies cleared after being spawned
        if(enemiesSpawned == true && enemies.Count == 0){
            //Unlock doors
            UnLockDoors();

            //Room is cleared
            isCleared = true;

            //Spawn chest by chance or if last room and no chests found
            SpawnChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player enters room, lock doors and spawn enemies
        if(collision.tag == "Player"){
            //Lock doors
            LockDoors();

            //Spawn enemies
            SpawnEnemies();
        }
    }

    private void SpawnEnemies(){
        foreach(GameObject spawnPoint in spawnPoints){
            //Spawn an enemy at spawn point
        }

        enemiesSpawned = true;
    }

    private void LockDoors(){
        foreach (Door door in doorScripts)
        {
            door.LockDoor();
        }
    }

    private void UnLockDoors()
    {
        foreach(Door door in doorScripts){
            door.UnLockDoor();
        }
    }

    void SpawnChest(){

    }
}
