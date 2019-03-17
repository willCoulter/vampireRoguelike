using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    void Awake()
    {
        instance = this;

        skill1Script = skill1Slot.GetComponent<SkillCooldown>();
        skill2Script = skill2Slot.GetComponent<SkillCooldown>();
        skill3Script = skill3Slot.GetComponent<SkillCooldown>();
    }

    [SerializeField]
    GameObject skill1Slot;

    [SerializeField]
    GameObject skill2Slot;

    [SerializeField]
    GameObject skill3Slot;

    private SkillCooldown skill1Script;
    private SkillCooldown skill2Script;
    private SkillCooldown skill3Script;

    //Called in skill inventory
    public void UpdateSkillSlot(int slotId)
    {
        switch (slotId)
        {
            case 1:
                skill1Script.instance.Initialize(SkillInventory.instance.skills[0], GameObject.FindGameObjectWithTag("Player"));
                break;
            case 2:
                skill2Script.instance.Initialize(SkillInventory.instance.skills[1], GameObject.FindGameObjectWithTag("Player"));
                break;
            case 3:
                skill3Script.instance.Initialize(SkillInventory.instance.skills[2], GameObject.FindGameObjectWithTag("Player"));
                break;
            default:
                return;
        }
    
    }
}
