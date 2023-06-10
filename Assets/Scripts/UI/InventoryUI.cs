using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MWFarm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip itemToolTip;
        [Header("��קͼƬ")]
        public Image DragItem;
        [Header("����״̬")]
        [SerializeField] private GameObject BagUI;
        private bool bagOpened;

        [Header("�������")]
        [SerializeField] private SlotUI[] playerSlots;

        private void Start()
        {
            //��ȡ��������״̬
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
        /// ʵʱ���·���UI
        /// </summary>
        /// <param name="location">�ֿ�����</param>
        /// <param name="list">��Ʒ����</param>
        private void onUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch(location)//��ʱֻ�����ﱳ��
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
        /// ����״̬���л�
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

