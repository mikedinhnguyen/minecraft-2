using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;

    private void Start()
    {
        holdingSlot = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
    }

    public void PressedDown()
    {
        if (Input.GetMouseButton(0))
        {
            if (gameObject.GetComponent<ItemSlot>() != null)
            {
                itemPending = gameObject.GetComponent<ItemSlot>();
            }
        }
        
    }

    public void PressedUp()
    {
        if (itemPending != null && itemPending.currentItem != null && itemPending.canBeHeld)
        {
            holdingSlot.currentItem = itemPending.currentItem;
        }
        else 
        {
            return;
        }
        holdingSlot.UpdateSlotData();
        //Debug.Log(itemPending + " held");
    }

    public void DropItem()
    {
        //ItemSlot craftingSlot = itemPending;
        // check to see if there is a held item and the craftingslot IS a crafting slot
        if (Input.GetMouseButton(0))
        {
            if (holdingSlot != null)
            {
                // drop item in the boxes
                itemPending.currentItem = holdingSlot.currentItem;
                itemPending.UpdateSlotData();
            }
        }
            
    }

    public void DebugTime()
    {
        Debug.Log(gameObject + " clicked");
    }
}
