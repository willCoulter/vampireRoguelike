using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSkillSlot : MonoBehaviour
{
    public Skill skill;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkill(Skill newSkill)
    {
        skill = newSkill;

        if(skill.skillSprite != null)
        {
            GetComponent<Image>().sprite = skill.skillSprite;
        }
    }

    public void ClearSkill()
    {
        if(skill != null)
        {
            GetComponent<Image>().sprite = null;
            skill = null;
        }
    }
}
