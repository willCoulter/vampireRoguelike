using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Keep track of how many chests dropped on this floor
    //Needed to gaurantee an item on last few rooms if none have dropped yet
    static int chestsDropped;

    [SerializeField]
    List<GameObject> doors;

    List<Enemy> enemies;
    List<GameObject> spawnPoints;

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
            //Room is cleared
            //Spawn chest by chance or if last room and no chests found
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            //Lock doors
            //Spawn enemies

        }
    }
}
