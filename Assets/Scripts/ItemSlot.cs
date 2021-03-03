using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public ItemSO currentItem;

    public Image itemImage;
    public RectTransform itemTransform;

    public Text count;
    public RectTransform countTransform;

    private CanvasGroup cg;
    public Canvas canvas;
    //bool _isHoldingItem;
    
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        UpdateSlotData();
        countTransform = count.GetComponent<RectTransform>();
        //_isHoldingItem = false;
    }

    public void UpdateSlotData()
    {
        if (currentItem != null)
        { // update item icons if they have an object in it
            itemImage.sprite = currentItem.itemIcon;
            count.enabled = true;
            count.text = "1";
        }
        else
        {
            itemImage.sprite = null;
            count.enabled = false;
        }
        
        itemTransform.anchoredPosition = Vector3.zero;
        //countTransform.anchoredPosition = Vector3.zero;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // don't pick up own item when searching in hover event
        //Debug.Log("down");
        cg.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("up");
        SwapItems(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("drag");
        if (currentItem != null)
        {
            itemTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            countTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void SwapItems(PointerEventData eventData)
    {
        bool foundSlot = false;

        foreach (GameObject overObj in eventData.hovered)
        {
            if (overObj != gameObject) // if the hovering item is on another item slot by mouse release
            { 

                if (overObj.GetComponent<ItemSlot>())
                {
                    ItemSlot itemSlot = overObj.GetComponent<ItemSlot>();

                    // swap item with current item
                    ItemSO previousItem = currentItem;
                    currentItem = itemSlot.currentItem;
                    itemSlot.currentItem = previousItem;

                    itemSlot.itemTransform.anchoredPosition = Vector3.zero; // set to center of box
                    itemSlot.UpdateSlotData();
                    UpdateSlotData();

                    itemSlot.countTransform.anchoredPosition = Vector3.zero;

                    foundSlot = true;
                }
            }
        }

        if (!foundSlot) // if there isn't a found slot, move item back to its origin
        {
            itemTransform.anchoredPosition = Vector3.zero;
            countTransform.anchoredPosition = Vector3.zero;
        }
        cg.blocksRaycasts = true;
    }

}
