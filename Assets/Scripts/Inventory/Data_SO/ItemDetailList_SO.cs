using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDetailList_SO", menuName = "Inventory/ItemDetailList_SO")]
public class ItemDetailList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailList;
}
