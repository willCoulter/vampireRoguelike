using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> bossEnemies;

    public bool isBossSpawner;

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

    public GameObject spawnBoss(int bossID)
    {
        GameObject boss;

        switch (bossID)
        {
            case 1:
                boss = bossEnemies[0];
                break;
            case 2:
                boss = bossEnemies[2];
                break;
            case 3:
                boss = bossEnemies[3];
                break;
            case 4:
                boss = bossEnemies[4];
                break;
            case 5:
                boss = bossEnemies[5];
                break;
            default:
                boss = bossEnemies[0];
                Debug.Log("Invalid boss ID");
                break;
        }

        GameObject returnBoss = Instantiate(boss, transform.position, transform.rotation);

        return returnBoss;
    }
}
