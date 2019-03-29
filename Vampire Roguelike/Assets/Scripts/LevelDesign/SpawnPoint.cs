using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject Mimic;
    
    public void spawnEnemy(int enemyID){

    }

    public GameObject spawnRandomEnemy(){
        GameObject randomEnemy;

        randomEnemy = Mimic;

        Instantiate(randomEnemy, transform.position, transform.rotation);

        return randomEnemy;
    }
}
