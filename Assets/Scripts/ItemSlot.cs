using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler , IDragHandler
{
    public ItemSO currentItem;

    public Image itemImage;
    public RectTransform itemTransform;

    public bool isCraftingSlot;
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
