using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public ItemSlot[] topRow = new ItemSlot[3];
    public ItemSlot[] middleRow = new ItemSlot[3];
    public ItemSlot[] bottomRow = new ItemSlot[3];

    private List<ItemSlot[]> allSlots = new List<ItemSlot[]>();

    [Space(10)]
    public ItemSlot outputSlot;

    private List<RecipeSO> recipes = new List<RecipeSO>();

    // Start is called before the first frame update
    void Start()
    {
        allSlots.Add(topRow);
        allSlots.Add(middleRow);
        allSlots.Add(bottomRow);

        recipes.AddRange(Resources.LoadAll<RecipeSO>("Recipes/"));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (RecipeSO recipe in recipes)
        {
            bool correctPlacement = true;

            List<ItemSO[]> allRecipeSlots = new List<ItemSO[]>();
            allRecipeSlots.Add(recipe.topRow);
            allRecipeSlots.Add(recipe.middleRow);
            allRecipeSlots.Add(recipe.bottomRow);

            // comparing the crafting item slots with recipes on hand and see if a crafting item is able to be made
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < allRecipeSlots[i].Length; j++)
                {
                    if (allRecipeSlots[i][j] != null)
                    {
                        if (allSlots[i][j].currentItem != null)
                        {
                            if (allRecipeSlots[i][j].itemName != allSlots[i][j].currentItem.itemName)
                            {
                                correctPlacement = false;
                            }
                        }
                        else
                        {
                            correctPlacement = false;
                        }
                    }
                    else
                    {
                        if (allSlots[i][j].currentItem != null)
                        {
                            correctPlacement = false;
                            continue;
                        }
                    }
                }
            }

            if (correctPlacement)
            {
                outputSlot.currentItem = recipe.output;
                outputSlot.UpdateSlotData();
                break;
            }
            else
            {
                outputSlot.currentItem = null;
                outputSlot.UpdateSlotData();
            }
        }
    }
}
