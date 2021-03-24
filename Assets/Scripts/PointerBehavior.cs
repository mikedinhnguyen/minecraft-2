using System.Collections;
using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;
    Transform inventory;

    private void Start()
    {
        holdingSlot = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
        inventory = GameObject.Find("Inventory").GetComponent<Transform>();
    }

    //IEnumerator Cooldown()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //}

    public void TouchDown()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (holdingSlot != null && itemPending.currentItem != holdingSlot.currentItem)
                {
                    // drop item in the boxes
                    itemPending.currentItem = holdingSlot.currentItem;
                    itemPending.UpdateSlotData();
                }
                else
                {
                    if (holdingSlot != null && itemPending.currentItem == holdingSlot.currentItem)
                    {
                        // pick up item in the box
                        itemPending.currentItem = null;
                        itemPending.UpdateSlotData();
                    }
                }
            }
        }
    }

    public void PressedDown()
    {
        if (Input.GetMouseButton(0))
        {
            //StartCoroutine(Cooldown());
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
            if (holdingSlot != null)
            {
                // find holding slot item, uncheck selector
                for (int i = 0; i < inventory.childCount; i++)
                {
                    ItemSlot item = inventory.GetChild(i).gameObject.GetComponent<ItemSlot>();
                    if (holdingSlot.currentItem == item.currentItem)
                    {
                        item.itemSelector.gameObject.SetActive(false);
                        break;
                    }
                }
            }

            holdingSlot.currentItem = itemPending.currentItem;
            itemPending.itemSelector.gameObject.SetActive(true);
        }
        else 
        {
            return;
        }
        holdingSlot.UpdateSlotData();
    }

    public void DropItem()
    {
        // check to see if there is a held item and the craftingslot IS a crafting slot
        if (Input.GetMouseButton(0))
        {
            if (holdingSlot != null && itemPending.currentItem != holdingSlot.currentItem)
            {
                // drop item in the boxes
                itemPending.currentItem = holdingSlot.currentItem;
                itemPending.UpdateSlotData();
            }
            else
            {
                if (holdingSlot != null && itemPending.currentItem == holdingSlot.currentItem)
                {
                    // pick up item in the box
                    itemPending.currentItem = null;
                    itemPending.UpdateSlotData();
                }
            }
        }
    }

    public void PickUpItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (holdingSlot != null && itemPending.currentItem == holdingSlot.currentItem)
            {
                // pick up item in the box
                itemPending.currentItem = null;
            }
            itemPending.UpdateSlotData();
        }
    }
}
