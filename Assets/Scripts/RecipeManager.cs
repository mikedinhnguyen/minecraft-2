using System;
using System.Collections.Generic;
using System.Linq;
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

    [HideInInspector]
    public int recipeValue;

    // Start is called before the first frame update
    void Start()
    {
        allSlots.Add(topRow);
        allSlots.Add(middleRow);
        allSlots.Add(bottomRow);

        recipes.AddRange(Resources.LoadAll<RecipeSO>("Recipes/"));
    }
    
    void Update()
    {
        foreach (RecipeSO recipe in recipes)
        {
            if (!recipe.isShapeless)
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
                    GetRecipeValue(recipe);
                    break;
                }
                else
                {
                    outputSlot.currentItem = null;
                    outputSlot.UpdateSlotData();
                }
            }
            else // is shapeless
            {
                List<ItemSO> check = new List<ItemSO>();
                List<ItemSO> current = new List<ItemSO>();

                for (int i = 0; i < recipe.shapelessIngredients.Length; i++)
                {
                    check.Add(recipe.shapelessIngredients[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (allSlots[i][j].currentItem != null)
                        {
                            current.Add(allSlots[i][j].currentItem);
                        }
                    }
                }

                ItemSO[] recipeArr = check.ToArray();
                ItemSO[] currArr = current.ToArray();

                Array.Sort(recipeArr, (x, y) => String.Compare(x.itemName, y.itemName));
                Array.Sort(currArr, (x, y) => String.Compare(x.itemName, y.itemName));

                if (Enumerable.SequenceEqual(recipeArr, currArr))
                {
                    outputSlot.currentItem = recipe.output;
                    outputSlot.UpdateSlotData();
                    GetRecipeValue(recipe);
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

    public void ClearAllSlots()
    {

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                allSlots[i][j].currentItem = null;
                allSlots[i][j].UpdateSlotData();
            }
        }
    }

    public void GetRecipeValue(RecipeSO recipe)
    {
        // number of different values used * number of slots used
        // grab a set of different itemso and find num
        // loop through recipe and find any null values that can be deducted from count

        HashSet<ItemSO> uniqueItems = new HashSet<ItemSO>();
        int slotsCount = 0;

        if (!recipe.isShapeless)
        {
            for (int i = 0; i < 3; i++)
            {
                if (recipe.topRow[i] != null)
                {
                    slotsCount++;
                    if (!uniqueItems.Contains(recipe.topRow[i]))
                    {
                        uniqueItems.Add(recipe.topRow[i]);
                    }
                }
                if (recipe.middleRow[i] != null)
                {
                    slotsCount++;
                    if (!uniqueItems.Contains(recipe.middleRow[i]))
                    {
                        uniqueItems.Add(recipe.middleRow[i]);
                    }
                }
                if (recipe.bottomRow[i] != null)
                {
                    slotsCount++;
                    if (!uniqueItems.Contains(recipe.bottomRow[i]))
                    {
                        uniqueItems.Add(recipe.bottomRow[i]);
                    }
                }
            }

            recipeValue = uniqueItems.Count * slotsCount;
        }
        else // shapeless
        {
            ItemSO[] items = new ItemSO[recipe.shapelessIngredients.Length];

            for (int i = 0; i < recipe.shapelessIngredients.Length; i++)
            {
                items[i] = recipe.shapelessIngredients[i];
            }

            foreach (ItemSO item in items)
            {
                if (item != null)
                {
                    slotsCount++;
                    if (!uniqueItems.Contains(item))
                    {
                        uniqueItems.Add(item);
                    }
                }
                
            }

            recipeValue = uniqueItems.Count * slotsCount;
        }

        // ie set of 2 items that use up 6 slots would value at 12
    }

    public void Hint(ItemSlot objective)
    {
        // grab objective recipe
        foreach (RecipeSO recipe in recipes)
        {
            if (objective.currentItem == recipe.output)
            {
                if (!recipe.isShapeless)
                {
                    List<ItemSO[]> allRecipeSlots = new List<ItemSO[]>();
                    allRecipeSlots.Add(recipe.topRow);
                    allRecipeSlots.Add(recipe.middleRow);
                    allRecipeSlots.Add(recipe.bottomRow);
                    // if the crafting table doesn't have a slot full where the ingredient should be, fill it
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < allRecipeSlots[i].Length; j++)
                        {
                            if (allRecipeSlots[i][j] != null)
                            {
                                if (allSlots[i][j].currentItem == null)
                                {
                                    allSlots[i][j].currentItem = allRecipeSlots[i][j];
                                    allSlots[i][j].UpdateSlotData();
                                    return;
                                }
                            }
                            else
                            {
                                if (allSlots[i][j].currentItem != null)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    return;
                }
                else // shapeless
                {
                    // TODO: shapeless hints
                    List<ItemSO> check = new List<ItemSO>();
                    List<ItemSO> current = new List<ItemSO>();

                    for (int i = 0; i < recipe.shapelessIngredients.Length; i++)
                    {
                        check.Add(recipe.shapelessIngredients[i]);
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (allSlots[i][j].currentItem != null)
                            {
                                current.Add(allSlots[i][j].currentItem);
                            }
                        }
                    }

                    foreach (ItemSO item in current)
                    {
                        if (check.Contains(item))
                        {
                            check.Remove(item);
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (allSlots[i][j].currentItem == null)
                            {
                                allSlots[i][j].currentItem = check[0];
                                allSlots[i][j].UpdateSlotData();
                                return;
                            }
                        }
                    }
                    return;
                }
            }
        }
    }

    public void FindRecipeValue(ItemSlot objective)
    {
        foreach (RecipeSO recipe in recipes)
        {
            if (objective.currentItem == recipe.output)
            {
                GetRecipeValue(recipe);
                return;
            }
        }
    }

}
