﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public static Store instance;

    public GameObject storeSkill1;
    public GameObject storeSkill2;

    public bool[] storeSkillsPurchased = new bool[2];

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        StorePickup storeSkill1Script = storeSkill1.GetComponent<StorePickup>();
        StorePickup storeSkill2Script = storeSkill2.GetComponent<StorePickup>();

        storeSkill1Script.skill.skillPrice = storeSkill1Script.price;
        storeSkill2Script.skill.skillPrice = storeSkill2Script.price;
    }

    // Update is called once per frame
    void Update()
    {
        if(storeSkill1 == null)
        {
            storeSkillsPurchased[0] = true;
        }
        else
        {
            storeSkillsPurchased[0] = false;
        }

        if (storeSkill2 == null)
        {
            storeSkillsPurchased[1] = true;
        }
        else
        {
            storeSkillsPurchased[1] = false;
        }
    }
}
