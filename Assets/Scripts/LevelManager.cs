using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public ItemSO[] chooseFrom;
    public Text score;
    public bool timeIsUp;

    ItemSlot itemOutput;
    ItemSlot objective;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        itemOutput = GameObject.Find("HoldingSlot").GetComponent<ItemSlot>();
        objective = GameObject.Find("ObjectiveSlot").GetComponent<ItemSlot>();
        timeIsUp = false;
        rand = Random.Range(0, chooseFrom.Length - 1);
        objective.currentItem = chooseFrom[rand];
        objective.UpdateSlotData();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemOutput.currentItem != null && objective.currentItem != null)
        {
            if (itemOutput.currentItem.itemName == objective.currentItem.itemName)
            {
                // pick next objective item
                rand = Random.Range(0, chooseFrom.Length);
                objective.currentItem = chooseFrom[rand];
                objective.UpdateSlotData();
                // score point
                ScorePoint();
            }
        }
        if (timeIsUp)
        {
            StopGame();
        }
    }

    void ScorePoint()
    {
        // update score
        int scoreInt = int.Parse(score.text);
        scoreInt += RecipeManager.recipeValue;
        score.text = scoreInt.ToString();
    }

    void StopGame()
    {
        // make everything not clickable

        // display end screen
    }
}
