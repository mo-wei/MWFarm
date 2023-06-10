using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace MWFarm.Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("�����Ϣ")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI slotNumText;
        public Image slotHighLight;
        [SerializeField] private Button button;

        [Header("��������")]
        public SlotType slotType;
        public bool isSelected;
        public int slotIndex;

        //������Ϣ
        public ItemDetails itemDetails;
        public int itemAmount;

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }
        /// <summary>
        /// ���¸�����Ϣ
        /// </summary>
        /// <param name="item">ItemDetails</param>
        /// <param name="amount">��������</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.enabled = true;
            slotImage.sprite = item.itemIcon;
            slotNumText.text = amount.ToString();
            button.interactable = true;
            itemAmount = amount;
        }

        /// <summary>
        /// ��Slot����Ϊ��
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }
            button.interactable = false;
            slotImage.enabled = false;
            slotNumText.text = string.Empty;
            itemAmount = 0;
            itemDetails = new ItemDetails();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount <= 0) return;
            isSelected = !isSelected;
            inventoryUI.UpdateSlotHighlight(slotIndex);

            if(slotType == SlotType.Bag)
            {
                EventHandler.CallItemSelectedEvent(itemDetails, isSelected);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(itemAmount > 0)
            {
                inventoryUI.DragItem.enabled = true;
                inventoryUI.DragItem.sprite = slotImage.sprite;
                inventoryUI.DragItem.SetNativeSize();

                isSelected = true;
                inventoryUI.UpdateSlotHighlight(slotIndex);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.enabled = false;
            //Debug.Log(eventData.pointerCurrentRaycast);

            if(eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;

                var targetItem = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetItem.slotIndex;
                if (targetItem.slotType == SlotType.Bag && slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
                //������и�����ʾ
                inventoryUI.UpdateSlotHighlight(-1);
            }
            /*else//��Ʒ��ק�����ϲ���(�������ԣ�����Ŀ�����д˹���)
            {
                if(itemDetails.canDropped)
                {
                    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    EventHandler.CallInstantiateItemInScence(itemDetails.itemID, pos);
                }
            }*/
        }
    }
}
