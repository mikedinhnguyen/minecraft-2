using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler , IDragHandler
{
    public ItemSO currentItem;

    public Image itemImage;
    public RectTransform itemTransform;

    private CanvasGroup cg;
    public Canvas canvas;

    public bool _pressed;
    public bool canBeHeld;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        UpdateSlotData();
        _pressed = false;
        canBeHeld = true;
        if (gameObject.name == "ObjectiveSlot")
        {
            canBeHeld = false;
        }
    }

    void Update()
    {
        
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
        //countTransform.anchoredPosition = Vector3.zero;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    // don't pick up own item when searching in hover event
    //    Debug.Log("down");
    //    cg.blocksRaycasts = false;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("up");
    //    SwapItems(eventData);
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    //Debug.Log("drag");
    //    if (currentItem != null)
    //    {
    //        itemTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    //        countTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    //    }
    //}

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

                    foundSlot = true;
                }
            }
        }

        if (!foundSlot) // if there isn't a found slot, move item back to its origin
        {
            itemTransform.anchoredPosition = Vector3.zero;
        }
        cg.blocksRaycasts = true;
    }

}
