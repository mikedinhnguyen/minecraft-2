using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItemManager/New Item")]
public class ItemSO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
}
