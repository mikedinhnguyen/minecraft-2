using System.Collections;
using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;
    Transform inventory;
    //bool touchedLastFrame = false;
    bool justPressed = false;

    private void Start()
    {
        holdingSlot = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
        if (transform.parent.parent.name == "Construction")
        {
            inventory = GameObject.Find("InventoryC").GetComponent<Transform>();
        }
        else if (transform.parent.parent.name == "Equipment")
        {
            inventory = GameObject.Find("InventoryE").GetComponent<Transform>();
        }
        else if (transform.parent.parent.name == "Items")
        {
            inventory = GameObject.Find("InventoryI").GetComponent<Transform>();
        }
        else if (transform.parent.parent.name == "Nature")
        {
            inventory = GameObject.Find("InventoryN").GetComponent<Transform>();
        }
        //inventory = GameObject.Find("Inventory").GetComponent<Transform>();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.2f);
        justPressed = false;
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
            LevelManager.tabWithItem = transform.parent.name;
            // Debug.Log("now holding item in " + LevelManager.tabWithItem);
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
        if (!justPressed && Input.GetMouseButton(0))
        {
            justPressed = true;

            if (holdingSlot != null && itemPending.currentItem == null)
            {
                // drop up item in the box if null
                itemPending.currentItem = holdingSlot.currentItem;
                itemPending.UpdateSlotData();
            }
            else if (holdingSlot != null && itemPending.currentItem == holdingSlot.currentItem)
            {
                // pick up item in the box
                itemPending.currentItem = null;
                itemPending.UpdateSlotData();
            }

            StartCoroutine(Cooldown());
        }
    }

    public void PickUpItem()
    {
        if (holdingSlot != null && itemPending.currentItem != holdingSlot.currentItem)
        {
            // pick up item in the box
            itemPending.currentItem = null;
        }
        itemPending.UpdateSlotData();
    }

    public void CategoryPick(Transform inventory)
    {
        if (holdingSlot.currentItem == null)
        {
            return;
        }
        // check if any items are part of holding
        LevelManager.CleanUpSelectors(inventory);
        for (int i = 0; i < inventory.childCount; i++)
        {
            ItemSlot item = inventory.GetChild(i).gameObject.GetComponent<ItemSlot>();
            if (holdingSlot.currentItem != null && item.currentItem == holdingSlot.currentItem)
            {
                item.itemSelector.gameObject.SetActive(true);
                item.UpdateSlotData();
                return;
            }
        }
    }
}
