using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float currentHealth;
    public float currentBlood;
    public int currentGold;
    public float[] position;
    public List<int> items;
    public List<int> skills;

    public PlayerData(PlayerController player)
    {
        items = new List<int>();
        skills = new List<int>();

        currentHealth = player.health;
        currentBlood = player.blood;
        currentGold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        
        foreach(Item item in ItemInventory.instance.items)
        {
            items.Add(item.itemID);
        }

        foreach(Skill skill in SkillInventory.instance.skills)
        {
            if(skill != null){
                skills.Add(skill.skillID);
            }
        }
    }
}
