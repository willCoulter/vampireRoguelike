using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GraveyardManager : MonoBehaviour
{
    public static GraveyardManager instance;
    public GameObject prefab;

    public GameObject recapMenu;
    public GameObject recapInfoWrapper;
    public GameObject recapTooltipWrapper;

    public Text levelNum;
    public Text enemiesSlain;
    public Text timeSurvived;

    private List<GraveyardSaveData> saveDatas;

    public GameObject pauseSkill1Slot;
    public GameObject pauseSkill2Slot;
    public GameObject pauseSkill3Slot;

    private PauseSkillSlot pauseSkill1Script;
    private PauseSkillSlot pauseSkill2Script;
    private PauseSkillSlot pauseSkill3Script;

    private List<PauseSkillSlot> pauseSkillScriptList = new List<PauseSkillSlot>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        pauseSkill1Script = pauseSkill1Slot.GetComponent<PauseSkillSlot>();
        pauseSkill2Script = pauseSkill2Slot.GetComponent<PauseSkillSlot>();
        pauseSkill3Script = pauseSkill3Slot.GetComponent<PauseSkillSlot>();

        pauseSkillScriptList.Add(pauseSkill1Script);
        pauseSkillScriptList.Add(pauseSkill2Script);
        pauseSkillScriptList.Add(pauseSkill3Script);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate()
    {
        saveDatas = SaveSystem.LoadGraveyardSaves();

        GameObject newItem;

        foreach(GraveyardSaveData saveData in saveDatas)
        {
            newItem = Instantiate(prefab, transform);

            GraveyardItem itemScript = newItem.GetComponent<GraveyardItem>();

            itemScript.UpdateSave(saveData);

            //Add listener to newly created item's button component to call Update on recap menu
            newItem.GetComponent<Button>().onClick.AddListener(delegate { UpdateRecapMenu(itemScript.graveyardSave); });
            newItem.GetComponent<Button>().onClick.AddListener(delegate { ToggleRecapInfo(true); });
        }
    }

    public void Clear()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateRecapMenu(GraveyardSaveData saveData)
    {
        //Clear skills
        SkillInventory.instance.skills[0] = null;
        SkillInventory.instance.skills[1] = null;
        SkillInventory.instance.skills[2] = null;

        //Clear pause slots
        foreach (PauseSkillSlot pauseSlot in pauseSkillScriptList)
        {
            pauseSlot.skill = null;
        }

        //Loop through saved skill id's
        foreach (int playerSkill in saveData.playerData.skills)
        {
            //Loop through all skills
            foreach (Skill skill in SkillInventory.instance.allSkills)
            {
                //Check if saved skill id matches a skill in all skills list
                if (playerSkill == skill.skillID)
                {
                    //If so, add to players current skills
                    SkillInventory.instance.Add(skill);
                }
            }
        }
        
        //Enable or disable ui element if no skill in slot
        foreach (PauseSkillSlot pauseSlot in pauseSkillScriptList)
        {
            //if not skill attached, disable
            if (pauseSlot.skill == null)
            {
                pauseSlot.gameObject.SetActive(false);
            }
            else if (pauseSlot.skill != null && pauseSlot.gameObject.activeSelf == false)
            {
                pauseSlot.gameObject.SetActive(true);
            }
        }

        //Clear items
        ItemInventory.instance.ClearAllItems();

        //Set player items
        foreach (int playerItem in saveData.playerData.items)
        {
            foreach (Item item in ItemInventory.instance.allItems)
            {
                if (playerItem == item.itemID)
                {
                    ItemInventory.instance.items.Add(item);
                }
            }
        }
        
        InventoryUI.instance.UpdateInventory();

        //Set text elements
        levelNum.text = "Level: " + saveData.levelData.levelNumber;
        enemiesSlain.text = "Enemies Slain: " + saveData.levelData.enemiesSlain;

        //Calculate and show time
        int seconds = Convert.ToInt32(saveData.levelData.timeSurvived % 60);
        TimeSpan timePlayedFormatted = TimeSpan.FromSeconds(seconds);
        timeSurvived.text = "Time: " + string.Format("{0:D2}:{1:D2}:{2:D2}", timePlayedFormatted.Hours, timePlayedFormatted.Minutes, timePlayedFormatted.Seconds);

    }

    public void UpdateSkillSlot(Skill skill, int slotId)
    {
        switch (slotId)
        {
            case 0:
                pauseSkill1Script.SetSkill(skill);
                break;
            case 1:
                pauseSkill2Script.SetSkill(skill);
                break;
            case 2:
                pauseSkill3Script.SetSkill(skill);
                break;
            default:
                return;
        }

    }

    public void ToggleRecapInfo(bool showInfo)
    {
        //If an item is selected, we want to set the information tab to active
        if(showInfo == true)
        {
            recapInfoWrapper.SetActive(true);
            recapTooltipWrapper.SetActive(false);
        }
        //If showinfo is false and no items in list, display graveyard empty tooltip
        else if(showInfo == false && saveDatas.Count == 0)
        {
            Text recapText = recapTooltipWrapper.GetComponentInChildren<Text>();

            recapText.text = "Graveyard Empty";

            recapInfoWrapper.SetActive(false);
            recapTooltipWrapper.SetActive(true);
        }else if(showInfo == false)
        {
            Text recapText = recapTooltipWrapper.GetComponentInChildren<Text>();

            recapText.text = "Pick a save to recap";

            recapInfoWrapper.SetActive(false);
            recapTooltipWrapper.SetActive(true);
        }
    }
}
