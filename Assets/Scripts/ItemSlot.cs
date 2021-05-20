using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemSO currentItem;

    public Image itemImage;
    public Image itemSelector;
    public RectTransform itemTransform;

    [HideInInspector]
    public bool isCraftingSlot;
    //[HideInInspector]
    public bool canBeHeld;

    void Start()
    {
        UpdateSlotData();
        isCraftingSlot = false;
        canBeHeld = true;
        itemSelector.gameObject.SetActive(false);
        if (gameObject.name == "ObjectiveSlot")
        {
            canBeHeld = false;
        } else
        {
            ObjectiveCheck objCheck = GameObject.Find("Objective").GetComponent<ObjectiveCheck>();
            objCheck.CheckForBaseItem();
        }
        if (gameObject.tag == "Crafting")
        {
            isCraftingSlot = true;
        }
    }

    public void UpdateSlotData()
    {
        if (currentItem != null)
        { // update item icons if they have an object in it
            itemImage.sprite = currentItem.itemIcon;
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.sprite = null;
            Color color = Color.gray;
            color.a = 0;
            itemImage.color = color;
        }
        
        itemTransform.anchoredPosition = Vector3.zero;
    }

}
