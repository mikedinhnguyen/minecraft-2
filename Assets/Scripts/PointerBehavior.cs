using System.Collections;
using UnityEngine;

public class PointerBehavior : MonoBehaviour
{
    ItemSlot holdingSlot;
    ItemSlot itemPending;
    Transform inventory;
    bool touchedLastFrame = false;
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

    public void TouchDown()
    {
        if (touchedLastFrame && Input.touchCount == 0)
        {
            touchedLastFrame = false;
        }
        else if (!touchedLastFrame && Input.touchCount > 0)
        {
            touchedLastFrame = true;
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
        if (!justPressed && Input.GetMouseButton(0))
        {
            justPressed = true;
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
            StartCoroutine(Cooldown());
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

    public void CategoryPick()
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

}
