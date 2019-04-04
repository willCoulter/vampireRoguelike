using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSkillSlot : MonoBehaviour
{
    public Skill skill;
    public GameObject infoBox;
    private GameObject infoBoxInstance;

    Text skillName;
    Text skillCost;
    Text skillDesc;
    Text skillCD;

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

    public void ShowSkillInfo()
    {
        //Instantiate box
        infoBoxInstance = Instantiate(infoBox, transform.parent.parent);

        //Grab child box, populate text fields
        Transform popupBox = infoBoxInstance.transform.Find("PopupBox");
        Text[] textItems = popupBox.GetComponentsInChildren<Text>();
        skillName = textItems[0];
        skillCost = textItems[1];
        skillCD = textItems[2];
        skillDesc = textItems[3];

        skillName.text = skill.skillName;
        skillCost.text = "Cost: " + skill.baseCost;
        skillCD.text = "CD: " + skill.baseCD;
        skillDesc.text = skill.desc;

        //Grab width and height
        RectTransform r = GetComponent<RectTransform>();
        float width = r.rect.width;
        float height = r.rect.height;

        //Offset infobox position to top right of inventory slot
        infoBoxInstance.transform.position = new Vector3(transform.position.x + (width / 4), transform.position.y + (height / 4));

        InventoryUI.instance.skillPopupBox = infoBoxInstance;
    }

    public void HideSkillInfo()
    {
        Destroy(infoBoxInstance);
        InventoryUI.instance.skillPopupBox = null;
    }
}
