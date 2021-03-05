using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PointerBehavior : MonoBehaviour
{
    private Vector3 mousePos;
    public float moveSpeed = 0.1f;
    bool hasItem;
    bool isHolding;
    ItemSlot itemInHand;
    ItemSlot itemPending;
    Sprite spriteIcon;

    private void Start()
    {
        hasItem = false;
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 2f;
        //If you get an error with the above line, replace it with this:
        //mousePosition = new Vector3(mousePosition.x, mousePosition.y, zAxis);

        if (spriteIcon != null && !isHolding)
        {
            //Vector2 pos = Vector2.right*(mousePos.x) + Vector2.up * ( mousePos.y);
            Debug.Log("Instantiate");
            Instantiate(spriteIcon, mousePos, Quaternion.identity);
            isHolding = true;
        }
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
        if (itemPending != null && itemPending.currentItem != null && !hasItem)
        {
            hasItem = true;
            itemInHand = itemPending;
            spriteIcon = itemInHand.currentItem.itemIcon;
        }
        else if (itemPending != null && itemPending.currentItem != null && hasItem)
        {
            // swap items
            itemInHand = itemPending;
        }
        else 
        {
            return;
        }
        Debug.Log(itemInHand.currentItem + " held");
    }

    public void DebugTime()
    {
        Debug.Log(gameObject + " clicked");
    }
}
