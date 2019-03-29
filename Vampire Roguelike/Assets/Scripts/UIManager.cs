using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    private List<SkillCooldown> cooldownScriptList = new List<SkillCooldown>();

    public GameObject skill1Slot;

    public GameObject skill2Slot;

    public GameObject skill3Slot;

    private SkillCooldown skill1Script;
    private SkillCooldown skill2Script;
    private SkillCooldown skill3Script;

    //Skill popup UI items
    public GameObject skillPopupBox;

    public GameObject skillName;

    public GameObject skillCost;

    public GameObject skillCD;

    public GameObject skillDesc;

    //Item popup UI items;
    public GameObject itemPopupBox;

    public GameObject itemName;

    public GameObject itemDesc;

    //Chest popup UI items;
    public GameObject chestPopupBox;

    //Pause menu items
    public GameObject pauseMenu;
    public Text LevelText;
    public Text EnemiesSlain;
    public Text TimeText;

    public bool pauseMenuEnabled;

    void Awake()
    {
        instance = this;

        skill1Script = skill1Slot.GetComponent<SkillCooldown>();
        skill2Script = skill2Slot.GetComponent<SkillCooldown>();
        skill3Script = skill3Slot.GetComponent<SkillCooldown>();

        cooldownScriptList.Add(skill1Script);
        cooldownScriptList.Add(skill2Script);
        cooldownScriptList.Add(skill3Script);
    }

    void Update()
    {
        if (cooldownScriptList.Count > 0)
        {
            UpdateSlotEnabled();
        }

        //Pause menu
        if (Input.GetButtonDown("Inventory") && pauseMenuEnabled == false)
        {
            pauseMenu.SetActive(true);
            pauseMenuEnabled = true;
            Time.timeScale = 0;
        }else if(Input.GetButtonDown("Inventory") && pauseMenuEnabled == true)
        {
            pauseMenu.SetActive(false);
            pauseMenuEnabled = false;
            Time.timeScale = 1;
        }
    }

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

    public void UpdateSlotEnabled()
    {
        foreach(SkillCooldown skillScript in cooldownScriptList)
        {
            //if no skill is attached to slot, disable
            if(skillScript.instance.skill == null)
            {
                skillScript.gameObject.SetActive(false);
            }
            //If there is a skill, and the slot is disabled, enable
            else if(skillScript.instance.skill != null && skillScript.gameObject.activeSelf == false)
            {
                skillScript.gameObject.SetActive(true);
            }
        }
    }

    public void displaySkillPopup(Skill skill, Vector3 popupPosition){
        skillName.GetComponent<Text>().text = skill.skillName;
        skillCost.GetComponent<Text>().text = "Cost: " + skill.baseCost;
        skillCD.GetComponent<Text>().text = "Cooldown: " + skill.baseCD;
        skillDesc.GetComponent<Text>().text = skill.desc;

        skillPopupBox.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(skillPopupBox.GetComponent<RectTransform>(), popupPosition);
    }

    public void hideSkillPopup(){
        skillPopupBox.SetActive(false);
    }

    public void displayItemPopup(Item item, Vector3 popupPosition){
        itemName.GetComponent<Text>().text = item.itemName;
        itemDesc.GetComponent<Text>().text = item.desc;

        itemPopupBox.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(itemPopupBox.GetComponent<RectTransform>(), popupPosition);
    }

    public void hideItemPopup()
    {
        itemPopupBox.SetActive(false);
    }

    public void displayChestPopup(Vector3 popupPosition){
        chestPopupBox.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(chestPopupBox.GetComponent<RectTransform>(), popupPosition);
    }

    public void hideChestPopup()
    {
        chestPopupBox.SetActive(false);
    }
}
