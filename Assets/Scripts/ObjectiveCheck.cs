using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCheck : MonoBehaviour
{
    public ItemSlot objSlot;
    public ItemSlot[] premadeItems;
    //public Transform[] inventories;
    
    public void CheckForBaseItem()
    {
        for (int i = 0; i < premadeItems.Length; i++)
        {
            if (objSlot.currentItem == premadeItems[i].currentItem)
            {
                //Debug.Log("Found " + objSlot.currentItem.itemName);

                CanvasRenderer cr = premadeItems[i].transform.Find("Icon").GetComponent<Image>().canvasRenderer;

                cr.SetColor(Color.gray);
                cr.SetAlpha(0.5f);
                premadeItems[i].canBeHeld = false;

            } else if (objSlot.currentItem != premadeItems[i].currentItem && premadeItems[i].currentItem != null)
            {
                CanvasRenderer cr = premadeItems[i].transform.Find("Icon").GetComponent<Image>().canvasRenderer;

                cr.SetColor(Color.white);
                cr.SetAlpha(1f);
                premadeItems[i].canBeHeld = true;
            }
            premadeItems[i].UpdateSlotData();
        }
    }
}
