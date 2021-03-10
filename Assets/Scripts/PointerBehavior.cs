using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;

    private void Start()
    {
        //hasItem = false;
        //isHolding = false;
        holdingSlot = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
    }

    public void PressedDown()
    {
        if (gameObject.GetComponent<ItemSlot>() != null)
        {
            itemPending = gameObject.GetComponent<ItemSlot>();
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
        ItemSlot craftingSlot = itemPending;
        // check to see if there is a held item and the craftingslot IS a crafting slot
        if (holdingSlot != null)
        {
            // drop item in the boxes
            craftingSlot.currentItem = holdingSlot.currentItem;
        }

        craftingSlot.UpdateSlotData();
    }

    // TODO:
    public void DropDragItems()
    {
        // check to see if there is a held item and the craftingslot IS a crafting slot

        // drop item in the boxes
    }

    public void DebugTime()
    {
        Debug.Log(gameObject + " clicked");
    }
}
