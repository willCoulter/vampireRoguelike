using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    public GameObject chest;

    public void SpawnChest(){
        float spawnChance;

        spawnChance = Random.Range(0, 10);

        //Spawn chest with 30% chance and if items gathered less than 4
        if(spawnChance > 7 && GameManager.instance.itemsGathered < 4)
        {
            Instantiate(chest, transform.position, transform.rotation);
        }

        //Also spawn chest if 2 rooms left and no items gathered
        if(GameManager.instance.roomsRemaining.Count < 3 && GameManager.instance.itemsGathered < 3)
        {
            Instantiate(chest, transform.position, transform.rotation);
        }
    }
}
