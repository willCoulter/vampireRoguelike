using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillInventory : MonoBehaviour
{

    public static SkillInventory instance;
    
    public List<Skill> allSkills;

    public Skill[] skills;

    void Awake()
    {
        instance = this;
        skills = new Skill[3];
    }

    public bool Add (Skill skill)
    {
        if(!SkillSlotAvailable())
        {
            Debug.Log("Not enough slots");
            return false;
        }

        int indexLocation = 0;
        bool skillSet = false;

        if(skills[0] == null)
        {
            skills[0] = skill;
            indexLocation = 0;
            skillSet = true;
        }else if(skills[1] == null)
        {
            skills[1] = skill;
            indexLocation = 1;
            skillSet = true;
        }
        else if(skills[2] == null)
        {
            skills[2] = skill;
            indexLocation = 2;
            skillSet = true;
        }

        if (skillSet && SceneManager.GetActiveScene().name != "MainMenu")
        {
            UIManager.instance.UpdateSkillSlot(skill, indexLocation);
            return true;
        }else if (skillSet && SceneManager.GetActiveScene().name == "MainMenu")
        {
            GraveyardManager.instance.UpdateSkillSlot(skill, indexLocation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Remove (int skillLocation)
    {
        switch (skillLocation)
        {
            case 1:
                skills[0] = null;
                break;
            case 2:
                skills[1] = null;
                break;
            case 3:
                skills[2] = null;
                break;
        }

    }

    private bool SkillSlotAvailable()
    {
        //If all three slots full, return false
        if(skills[0] != null && skills[1] != null && skills[2] != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
