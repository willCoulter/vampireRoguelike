using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{

    public static SkillInventory instance;
    public int slots = 3;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory instance");
            return;
        }

        instance = this;
    }

    public List<Skill> skills = new List<Skill>();

    public bool Add (Skill skill)
    {
        if(skills.Count >= slots)
        {
            Debug.Log("Not enough slots");
            return false;
        }

        skills.Add(skill);
        UIManager.instance.UpdateSkillSlot(skills.Count);

        return true;
    }

    public void Remove (Skill skill)
    {
        skills.Remove(skill);
    }
}
