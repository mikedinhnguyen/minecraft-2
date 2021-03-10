using System.Collections.Generic;
using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;
    List<ItemSlot> itemsPending;

    private void Start()
    {
        //hasItem = false;
        //isHolding = false;
        holdingSlot = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
        itemsPending = new List<ItemSlot>();
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

    public void PressedDownMultiple()
    {
        if (gameObject.GetComponent<ItemSlot>() != null)
        {
            itemsPending.Add(gameObject.GetComponent<ItemSlot>());
        }
    }

    public void DropMultipleItems()
    {
        List<ItemSlot> craftingSlot = itemsPending;

        if (holdingSlot != null)
        {
            for (int i = 0; i < itemsPending.Capacity; i++)
            {
                craftingSlot[i].currentItem = holdingSlot.currentItem;
                craftingSlot[i].UpdateSlotData();
            }
        }
        
    }

    public void DropItem()
    {
        ItemSlot craftingSlot = itemPending;
        // check to see if there is a held item and the craftingslot IS a crafting slot
        if (holdingSlot != null)
        {
            // drop item in the boxes
            craftingSlot.currentItem = holdingSlot.currentItem;
            craftingSlot.UpdateSlotData();
        }
    }

    public void DebugTime()
    {
        Debug.Log(gameObject + " clicked");
    }
}
