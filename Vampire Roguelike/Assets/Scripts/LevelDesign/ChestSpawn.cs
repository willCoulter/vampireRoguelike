using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    public GameObject chest;

    public void SpawnChest(){
        Instantiate(chest, transform.position, transform.rotation);
    }
}
