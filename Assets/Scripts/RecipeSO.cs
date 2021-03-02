using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ItemManager/New Recipe")]
public class RecipeSO : ScriptableObject
{
    public ItemSO[] topRow = new ItemSO[3];
    public ItemSO[] middleRow = new ItemSO[3];
    public ItemSO[] bottomRow = new ItemSO[3];

    public ItemSO output;
}