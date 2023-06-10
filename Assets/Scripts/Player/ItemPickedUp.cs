using UnityEngine;
using MWFarm.Inventory;
public class ItemPickedUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if(item != null)
        {
            if(item.itemDetails.canPickedup)
            {
                InventoryManager.Instance.AddItem(item, true);
            }
        }
    }
}
