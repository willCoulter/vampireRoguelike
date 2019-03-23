using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Keep track of how many chests dropped on this floor
    //Needed to gaurantee an item on last few rooms if none have dropped yet
    static int chestsDropped;

    public List<GameObject> doors;
    public List<GameObject> spawnPoints;

    List<Enemy> enemies;

    Transform chestLocation;

    bool isCleared;
    bool enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
       
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
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().LockDoor();
        }
    }

    private void UnLockDoors()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().UnLockDoor();
        }
    }

    void SpawnChest(){

    }
}
