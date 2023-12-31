using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Text valueText;
    [SerializeField] private GameObject buttomPart;

    public void SetUpToolTip(ItemDetails itemDetails, SlotType slotType)
    {
        nameText.text = itemDetails.itemName;

        typeText.text = GetItemType(itemDetails.itemType);

        descriptionText.text = itemDetails.itemDescription;

        if(itemDetails.itemType == ItemType.Commodity || itemDetails.itemType == ItemType.Furniture || itemDetails.itemType == ItemType.Seed)
        {
            buttomPart.SetActive(true);

            int price = itemDetails.itemPrice;
            if(slotType == SlotType.Bag)
            {
                price = (int)(price * itemDetails.sellPercentage);
            }

            valueText.text = price.ToString();
        }
        else
        {
            buttomPart.SetActive(false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    
    private string GetItemType(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Seed => "种子",
            ItemType.Commodity => "商品",
            ItemType.Furniture => "家具",
            ItemType.BreakTool => "工具",
            ItemType.ChopTool => "工具",
            ItemType.CollectionTool => "工具",
            ItemType.HoeTool => "工具",
            ItemType.ReapTool => "工具",
            ItemType.WaterTool => "工具",
            _ => "无"

        };

    }
}
