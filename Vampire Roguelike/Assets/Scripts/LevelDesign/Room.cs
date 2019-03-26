using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Keep track of how many chests dropped on this floor
    //Needed to gaurantee an item on last few rooms if none have dropped yet
    static int chestsDropped;

    public int roomID;

    public List<GameObject> doors;
    public List<GameObject> spawnPoints;
    public List<GameObject> enemies = new List<GameObject>();

    public GameObject chestSpawn;

    bool isCleared;
    bool enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        isCleared = false;
        enemiesSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If enemies are spawned and room is not cleared
        if(isCleared == false && enemiesSpawned == true){

            //Keep track of enemy count
            Debug.Log(enemies.Count);

            //If all enemies dead
            if (enemies.Count == 0)
            {
                Debug.Log("Doors actually unlocked");
                //Unlock doors
                UnLockDoors();

                //Room is cleared
                isCleared = true;

                //Spawn chest by chance or if last room and no chests found
                SpawnChest();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If player enters room, lock doors and spawn enemies
        if(collision.tag == "Player" && enemiesSpawned == false){
            //Lock doors
            Invoke("LockDoors", 0.2f);
            Debug.Log("Room " + roomID + " doors locked");

            //Spawn enemies
            SpawnEnemies();
        }
    }

    private void SpawnEnemies(){
        foreach(GameObject spawnPoint in spawnPoints){
            //Spawn random enemy at spawn point
            GameObject enemy = spawnPoint.GetComponent<SpawnPoint>().spawnRandomEnemy();
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            //Add listener to call enemyslain method when gameobject destroyed
            enemyScript.OnDestroy.AddListener(delegate{EnemySlain(enemyScript.gameObject);});

            //Add to enemy list
            enemies.Add(enemy);
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

    //Will not call
    public void EnemySlain(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    void SpawnChest(){
        chestSpawn.GetComponent<ChestSpawn>().SpawnChest();
    }
}
