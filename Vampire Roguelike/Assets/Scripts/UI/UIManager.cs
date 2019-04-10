using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public GameObject canvas;

    //Skill popup UI items
    public GameObject skillPopupBox;

    public GameObject skillName;

    public GameObject skillCost;

    public GameObject skillCD;

    public GameObject skillDesc;

    public GameObject skillPrice;

    public GameObject skillButtonPrompt;

    //Item popup UI items;
    public GameObject itemPopupBox;

    public GameObject itemName;

    public GameObject itemDesc;

    public GameObject itemButtonPrompt;

    //Chest popup UI items;
    public GameObject chestPopupBox;

    //Pause menu UI items;
    public Image pauseBG;
    public GameObject pauseMenu;
    public Text topBoxText;
    public Text level;
    public Text enemiesSlain;
    public Text time;
    public bool pauseMenuOpen;
    public Image pauseTopBox;

    public GameObject pauseButtonsWrapper;
    public GameObject deathButtonsWrapper;

    public GameObject pauseSkill1Slot;
    public GameObject pauseSkill2Slot;
    public GameObject pauseSkill3Slot;

    private PauseSkillSlot pauseSkill1Script;
    private PauseSkillSlot pauseSkill2Script;
    private PauseSkillSlot pauseSkill3Script;

    private List<PauseSkillSlot> pauseSkillScriptList = new List<PauseSkillSlot>();
    
    public AudioSource gameAudioManager;

    public Sprite deathSpriteBG;
    public Sprite deathSpriteTop;

    void Awake()
    {
        instance = this;

        skill1Script = skill1Slot.GetComponent<SkillCooldown>();
        skill2Script = skill2Slot.GetComponent<SkillCooldown>();
        skill3Script = skill3Slot.GetComponent<SkillCooldown>();

        cooldownScriptList.Add(skill1Script);
        cooldownScriptList.Add(skill2Script);
        cooldownScriptList.Add(skill3Script);

        pauseSkill1Script = pauseSkill1Slot.GetComponent<PauseSkillSlot>();
        pauseSkill2Script = pauseSkill2Slot.GetComponent<PauseSkillSlot>();
        pauseSkill3Script = pauseSkill3Slot.GetComponent<PauseSkillSlot>();

        pauseSkillScriptList.Add(pauseSkill1Script);
        pauseSkillScriptList.Add(pauseSkill2Script);
        pauseSkillScriptList.Add(pauseSkill3Script);
    }

    void Update()
    {
        UpdateSlotsEnabled();

        //If player presses tab, open or close pause menu
        if (Input.GetKeyDown("tab") && PlayerController.instance.health > 0)
        {
            if (!pauseMenuOpen)
            {
                DisplayPauseMenu();
            }
            else
            {
                HidePauseMenu();
            }
        }
    }

    public void RefreshPauseSkills()
    {
        foreach(SkillCooldown skillScript in cooldownScriptList)
        {
            //If skill slot has a skill in it, update pause skill slot
            if (skillScript.instance.skill != null)
            {
                //Grab current index
                int skillIndex = cooldownScriptList.IndexOf(skillScript);

                //Set skill for slot at same index position
                pauseSkillScriptList[skillIndex].SetSkill(skillScript.skill);
            }
            else
            {
                //Grab current index
                int skillIndex = cooldownScriptList.IndexOf(skillScript);

                //Clear skill at position
                pauseSkillScriptList[skillIndex].ClearSkill();
            }
        }
    }

    public void DisplayDeathMenu()
    {
        //pauseBG.sprite = deathSpriteBG;
        //pauseTopBox.sprite = deathSpriteTop;

        //topBoxText.text = "You died";

        DisplayPauseMenu();
        pauseButtonsWrapper.SetActive(false);
        deathButtonsWrapper.SetActive(true);

        if(gameAudioManager != null)
        {
            gameAudioManager.Stop();
        }
    }

    private void DisplayPauseMenu()
    {
        //Set values
        enemiesSlain.text = "Enemies Slain: " + GameManager.instance.enemiesSlain;
        level.text = "Level: " + GameManager.instance.currentLevelNum;

        //Refresh inventory
        InventoryUI.instance.UpdateInventory();

        RefreshPauseSkills();
        
        pauseMenu.SetActive(true);
        pauseMenuOpen = true;
        Time.timeScale = 0;
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        pauseMenuOpen = false;
        Time.timeScale = 1;
    }

    //Called in skill inventory
    public void UpdateSkillSlot(Skill skill, int slotId)
    {
        switch (slotId)
        {
            case 0:
                skill1Script.instance.Initialize(skill, GameObject.FindGameObjectWithTag("Player"));
                break;
            case 1:
                skill2Script.instance.Initialize(skill, GameObject.FindGameObjectWithTag("Player"));
                break;
            case 2:
                skill3Script.instance.Initialize(skill, GameObject.FindGameObjectWithTag("Player"));
                break;
            default:
                return;
        }
    
    }

    public void UpdateSlotsEnabled()
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

        foreach(PauseSkillSlot pauseSlot in pauseSkillScriptList)
        {
            //if not skill attached, disable
            if(pauseSlot.skill == null)
            {
                pauseSlot.gameObject.SetActive(false);
                pauseSlot.dropButton.SetActive(false);
            }
            else if (pauseSlot.skill != null && pauseSlot.gameObject.activeSelf == false)
            {
                pauseSlot.gameObject.SetActive(true);
                pauseSlot.dropButton.SetActive(true);
            }
        }
    }

    public void displaySkillPopup(Skill skill, Vector3 popupPosition){
        skillName.GetComponent<Text>().text = skill.skillName;
        skillCost.GetComponent<Text>().text = "Cost: " + skill.baseCost;
        skillCD.GetComponent<Text>().text = "CD: " + skill.baseCD + "s";
        skillDesc.GetComponent<Text>().text = skill.desc;
        skillButtonPrompt.GetComponent<Text>().text = "Press " + PlayerController.instance.playerControls["Interact"].ToString() + " to pick up."; 
        if (skill.skillPrice > 0) {
            skillPrice.GetComponent<Text>().text = "Gold Price: " + skill.skillPrice.ToString();
            skillButtonPrompt.GetComponent<Text>().text = "Press " + PlayerController.instance.playerControls["Interact"].ToString() + " to buy.";
        }
        

        skillPopupBox.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(skillPopupBox.GetComponent<RectTransform>(), popupPosition);
    }

    public void hideSkillPopup(){
        skillPopupBox.SetActive(false);
    }

    public void displayItemPopup(Item item, Vector3 popupPosition){
        itemName.GetComponent<Text>().text = item.itemName;
        itemDesc.GetComponent<Text>().text = item.desc;
        itemButtonPrompt.GetComponent<Text>().text = "Press " + PlayerController.instance.playerControls["Interact"].ToString() + " to pick up.";
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
