using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardItem : MonoBehaviour
{
    public GraveyardSaveData graveyardSave;
    private Text deathDate;
    private Text survivalTime;

    void Awake()
    {
        
    }

    public void UpdateSave(GraveyardSaveData save)
    {
        Text[] textItems = transform.GetComponentsInChildren<Text>();
        deathDate = textItems[0];
        survivalTime = textItems[1];

        graveyardSave = save;
        deathDate.text = "" + save.finalDeathTime;
        survivalTime.text = GetLevel();
    }

    public string GetLevel()
    {
        return "Level: " + graveyardSave.levelData.levelNumber;
    }

    public string GetEnemiesSlain()
    {
        return "Enemies Slain: " + graveyardSave.levelData.enemiesSlain;
    }

    public string GetTimeSurvived()
    {
        return "Time survived: " + graveyardSave.levelData.timeSurvived;
    }

    public List<Skill> GetSkills()
    {
        List<Skill> skillList = new List<Skill>();

        //Loop through saved skill id's
        foreach (int playerSkill in graveyardSave.playerData.skills)
        {
            //Loop through all skills
            foreach (Skill skill in SkillInventory.instance.allSkills)
            {
                //Check if saved skill id matches a skill in all skills list
                if (playerSkill == skill.skillID)
                {
                    //If so, add to list
                    skillList.Add(skill);
                }
            }
        }

        return skillList;
    }

    public List<Item> GetItems()
    {
        List<Item> itemList = new List<Item>();

        //Get player items
        foreach (int playerItem in graveyardSave.playerData.items)
        {
            foreach (Item item in ItemInventory.instance.allItems)
            {
                if (playerItem == item.itemID)
                {
                    itemList.Add(item);
                }
            }
        }

        return itemList;
    }
}
