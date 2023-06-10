using UnityEngine;
using MWFarm.Inventory;

public class Item : MonoBehaviour
{
    public int itemID;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    public ItemDetails itemDetails;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if(itemID != 0)
        {
            Init();
        }
    }
    private void Init()
    {
        //Inventory获得当前数据
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

        if(itemDetails != null)
        {
            spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;

            //修改碰撞体尺寸
            Vector2 newSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
            coll.size = newSize;
            coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
        }
    }
}
