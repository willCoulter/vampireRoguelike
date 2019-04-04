using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public GameObject infoBox;
    private GameObject infoBoxInstance;

    Text itemName;
    Text itemDesc;

    public Image icon;

    Item item;

    private void Start()
    {
        
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void ShowItemInfo(){
        //Instantiate box
        infoBoxInstance = Instantiate(infoBox, transform.parent.parent);

        //Grab child box, populate text fields
        Transform popupBox = infoBoxInstance.transform.Find("PopupBox");
        Text[] textItems = popupBox.GetComponentsInChildren<Text>();
        itemName = textItems[0];
        itemDesc = textItems[2];

        itemName.text = item.itemName;
        itemDesc.text = item.desc;

        //Grab width and height
        RectTransform r = GetComponent<RectTransform>();
        float width = r.rect.width;
        float height = r.rect.height;

        //Offset infobox position to top right of inventory slot
        infoBoxInstance.transform.position = new Vector3(transform.position.x + (width / 4), transform.position.y + (height / 4));
        
        InventoryUI.instance.itemPopupBox = infoBoxInstance;
    }

    public void HideItemInfo(){
        Destroy(infoBoxInstance);
        InventoryUI.instance.itemPopupBox = null;
    }
}
