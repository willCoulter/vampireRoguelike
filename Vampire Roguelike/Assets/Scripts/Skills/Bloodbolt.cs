﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodbolt : MonoBehaviour
{
    public float damage = 10;
    public Transform launchPosition;
    public GameObject bloodBoltPrefab;


    public void TriggerBolt()
    {
        GameObject bullet = Instantiate(bloodBoltPrefab, launchPosition.position, launchPosition.rotation);
        Debug.Log("Instantiated");

    }
}
