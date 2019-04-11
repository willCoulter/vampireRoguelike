using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Keep track of how many chests dropped on this floor
    //Needed to gaurantee an item on last few rooms if none have dropped yet
    static int chestsDropped;

    public int roomID;

    public List<GameObject> doors = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    public GameObject doorWrapper;
    public GameObject spawnWrapper;
    public GameObject chestSpawn;

    public GameObject bossSpawnPoint;
    public GameObject stairSpawn;
    public GameObject stairPrefab;

    public bool isCleared;
    public bool isBossRoom;
    bool enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        enemiesSpawned = false;

        //Set doors
        foreach(Transform child in doorWrapper.transform)
        {
            doors.Add(child.gameObject);
        }

        //Set spawns
        foreach(Transform child in spawnWrapper.transform)
        {
            spawnPoints.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //If enemies are spawned and room is not cleared
        if(isCleared == false && enemiesSpawned == true){

            //Keep track of enemy count
            //Debug.Log(enemies.Count);

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
        if(collision.tag == "Player" && enemiesSpawned == false && isCleared == false){
            //Lock doors
            Invoke("LockDoors", 0.2f);
            Debug.Log("Room " + roomID + " doors locked");

            //Spawn enemies
            SpawnEnemies();
        }
    }

    private void SpawnEnemies(){
        if (isBossRoom)
        {
            GameObject boss = bossSpawnPoint.GetComponent<SpawnPoint>().spawnBoss(GameManager.instance.currentLevelNum);
        }
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

    void SpawnStairs()
    {
        Instantiate(stairPrefab, stairSpawn.transform.position, stairSpawn.transform.rotation);
    }
}
