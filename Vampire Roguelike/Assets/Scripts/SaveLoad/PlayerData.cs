using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float currentHealth;
    public float currentBlood;
    public float currentGold;
    public float[] position;
    public List<Item> items;
    public List<Skill> skills;

    public PlayerData(PlayerController player)
    {
        currentHealth = player.health;
        currentBlood = player.blood;
        currentGold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        items = ItemInventory.instance.items;
        skills = SkillInventory.instance.skills;
    }
}
