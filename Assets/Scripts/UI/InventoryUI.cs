using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MWFarm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip itemToolTip;
        [Header("拖拽图片")]
        public Image DragItem;
        [Header("背包状态")]
        [SerializeField] private GameObject BagUI;
        private bool bagOpened;

        [Header("方格管理")]
        [SerializeField] private SlotUI[] playerSlots;

        private void Start()
        {
            //获取背包开关状态
            bagOpened = BagUI.activeInHierarchy;

            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                BagStateChange();
            }
        }

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += onUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= onUpdateInventoryUI;
        }
        /// <summary>
        /// 实时更新方格UI
        /// </summary>
        /// <param name="location">仓库类型</param>
        /// <param name="list">物品数据</param>
        private void onUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch(location)//暂时只有人物背包
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if(list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
                
                default:
                    break;
            }
        }
        /// <summary>
        /// 背包状态的切换
        /// </summary>
        public void BagStateChange()
        {
            bagOpened = !bagOpened;

            BagUI.SetActive(bagOpened);
        }

        public void UpdateSlotHighlight(int index)
        {
            foreach (var slot in playerSlots)
            {
                if(slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHighLight.gameObject.SetActive(true);
                }
                else
                {
                    slot.slotHighLight.gameObject.SetActive(false);
                    slot.isSelected = false;
                }
            }
        }
    }
}

