
using UnityEngine;

namespace MWFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDetailList_SO itemDetailList_SO;

        [Header("背包数据")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.inventoryItems);
        }
        /// <summary>
        /// 通过物品ID获取详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDetailList_SO.itemDetailList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// 添加物品到背包里面
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">是否销毁物品</param>
        public void AddItem(Item item, bool toDestory)
        {
            var index = GetItemIndexInBag(item.itemID);

            AddItemAtIndex(item.itemID, index, 1);

            print(GetItemDetails(item.itemID).itemName + "  " + GetItemDetails(item.itemID).itemDescription);
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.inventoryItems);
            if (toDestory)
            {
                Destroy(item.gameObject);
            }

            
        }
        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for(int i = 0; i < playerBag.inventoryItems.Count; i++)
            {
                if (playerBag.inventoryItems[i].itemID == 0)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 找到物品在背包中的位置
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1代表背包中不存在对应物品</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.inventoryItems.Count; i++)
            {
                if (playerBag.inventoryItems[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        private void AddItemAtIndex(int ID, int index, int amount)
        {
            //背包中不存在并且有空位
            if(index == -1 && CheckBagCapacity())
            {
                InventoryItem newItem = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.inventoryItems.Count; i++)
                {
                    if (playerBag.inventoryItems[i].itemID == 0)
                    {
                        playerBag.inventoryItems[i] = newItem;
                        break;
                    }
                }
            }
            else
            {
                int newAmount = playerBag.inventoryItems[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = newAmount };

                playerBag.inventoryItems[index] = item;
            }
        }
        /// <summary>
        /// 交换背包中两个数据
        /// </summary>
        /// <param name="fromIndex">拖拽的物品</param>
        /// <param name="targetIndex">目标物品</param>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            if(playerBag.inventoryItems[targetIndex].itemID == 0)//目标没有物品
            {
                playerBag.inventoryItems[targetIndex] = playerBag.inventoryItems[fromIndex];
                playerBag.inventoryItems[fromIndex] = new InventoryItem();
            }
            else
            {
                InventoryItem fromItem = playerBag.inventoryItems[fromIndex];
                InventoryItem targetItem = playerBag.inventoryItems[targetIndex];
                playerBag.inventoryItems[fromIndex] = targetItem;
                playerBag.inventoryItems[targetIndex] = fromItem;
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.inventoryItems);
        }
    }
}


