
using UnityEngine;

namespace MWFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDetailList_SO itemDetailList_SO;

        [Header("��������")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.inventoryItems);
        }
        /// <summary>
        /// ͨ����ƷID��ȡ��ϸ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDetailList_SO.itemDetailList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// �����Ʒ����������
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">�Ƿ�������Ʒ</param>
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
        /// ��鱳���Ƿ��п�λ
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
        /// �ҵ���Ʒ�ڱ����е�λ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1�������в����ڶ�Ӧ��Ʒ</returns>
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
            //�����в����ڲ����п�λ
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
        /// ������������������
        /// </summary>
        /// <param name="fromIndex">��ק����Ʒ</param>
        /// <param name="targetIndex">Ŀ����Ʒ</param>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            if(playerBag.inventoryItems[targetIndex].itemID == 0)//Ŀ��û����Ʒ
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


