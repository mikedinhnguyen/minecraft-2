using UnityEngine;

public class LevelManager : MonoBehaviour
{
    ItemSlot itemOutput;
    ItemSlot objective;
    // Start is called before the first frame update
    void Start()
    {
        itemOutput = GameObject.Find("OutputSlot").GetComponent<ItemSlot>();
        objective = GameObject.Find("ObjectiveSlot").GetComponent<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemOutput.currentItem != null && objective.currentItem != null)
        {
            if (itemOutput.currentItem.itemName == objective.currentItem.itemName)
            {
                Debug.Log("Objective complete!");
            }
        }

    }
}
