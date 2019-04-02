using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemies;
    
    public void spawnEnemy(int enemyID){

    }

    public GameObject spawnRandomEnemy(){
        GameObject randomEnemy;

        //Generate random enemy
        randomEnemy = enemies[Random.Range(0, enemies.Count - 1)];

        //Instantiate random enemy at spawn position
        GameObject returnEnemy = Instantiate(randomEnemy, transform.position, transform.rotation);

        return returnEnemy;
    }
}
