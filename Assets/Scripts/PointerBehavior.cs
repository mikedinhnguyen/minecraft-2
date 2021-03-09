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
    
    void Update()
    {
        
    }

    public void PressedDown()
    {
        if (gameObject.GetComponent<ItemSlot>() != null)
        {
            Debug.Log("down");
            itemPending = gameObject.GetComponent<ItemSlot>();
        }
    }

    public void PressedUp()
    {
        if (itemPending != null && itemPending.currentItem != null && itemPending.canBeHeld)
        {
            //hasItem = true;
            Debug.Log("up");
            //itemInHand = itemPending;
            //spriteIcon = itemInHand.GetComponentInChildren<Rigidbody2D>();
            holdingSlot.currentItem = itemPending.currentItem;
        }
        //else if (itemPending != null && itemPending.currentItem != null && hasItem)
        //{
        //    // swap items
        //    itemInHand = itemPending;
        //}
        else 
        {
            return;
        }
        holdingSlot.UpdateSlotData();
        Debug.Log(itemPending + " held");
    }

    public void DebugTime()
    {
        Debug.Log(gameObject + " clicked");
    }
}
