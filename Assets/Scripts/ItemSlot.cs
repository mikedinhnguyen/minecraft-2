using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemSO currentItem;

    public Image itemImage;
    public RectTransform itemTransform;

    [HideInInspector]
    public bool isCraftingSlot;
    [HideInInspector]
    public bool canBeHeld;

    void Start()
    {
        UpdateSlotData();
        isCraftingSlot = false;
        canBeHeld = true;
        if (gameObject.name == "ObjectiveSlot")
        {
            canBeHeld = false;
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
        }
        else
        {
            itemImage.sprite = null;
        }
        
        itemTransform.anchoredPosition = Vector3.zero;
    }

}
