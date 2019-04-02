using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{

    public static SkillInventory instance;

    public List<Skill> skills = new List<Skill>();

    private Skill skill1;
    private Skill skill2;
    private Skill skill3;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory instance");
            return;
        }

        instance = this;

        skills.Add(skill1);
        skills.Add(skill2);
        skills.Add(skill3);
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

        if(skill1 == null)
        {
            skill1 = skill;
            indexLocation = 0;
            skillSet = true;
        }else if(skill2 == null)
        {
            skill2 = skill;
            indexLocation = 1;
            skillSet = true;
        }
        else if(skill3 == null)
        {
            skill3 = skill;
            indexLocation = 2;
            skillSet = true;
        }

        if (skillSet)
        {
            UIManager.instance.UpdateSkillSlot(skill, indexLocation);
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
                skill1 = null;
                break;
            case 2:
                skill2 = null;
                break;
            case 3:
                skill3 = null;
                break;
        }

    }

    private bool SkillSlotAvailable()
    {
        //If all three slots full, return false
        if(skill1 != null && skill2 != null && skill3 != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
