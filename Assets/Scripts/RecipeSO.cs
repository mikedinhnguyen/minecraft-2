using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ItemManager/New Recipe")]
public class RecipeSO : ScriptableObject
{
    public ItemSO[] topRow = new ItemSO[3];
    public ItemSO[] middleRow = new ItemSO[3];
    public ItemSO[] bottomRow = new ItemSO[3];

    public bool isShapeless;
    public ItemSO[] shapelessIngredients;

    public ItemSO output;
}
