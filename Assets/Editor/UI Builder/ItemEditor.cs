using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;

public class ItemEditor : EditorWindow
{
    private ItemDetailList_SO dataBase;

    private List<ItemDetails> itemList = new List<ItemDetails>();

    private VisualTreeAsset itemRowTemplate;

    private ListView itemListView;

    private ScrollView itemDetailsSection;

    private ItemDetails activeItem;

    //Ĭ��ͼƬ����
    private Sprite defaultIcon;

    private VisualElement iconPreview;


    [MenuItem("MWFarm/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        /*VisualElement label = new Label("Hello World! From C#");
        root.Add(label);*/

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        //ģ������
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemTemplate.uxml");

        //������ֵ
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPreview = itemDetailsSection.Q<VisualElement>("Icon");

        //Ĭ��ͼƬ��ֵ
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");

        
        //��������
        LoadDataBase();

        GenerateListView();

        //�󶨰���
        root.Q<Button>("DeleteButton").clicked += OnDeleteClicked;
        root.Q<Button>("AddButton").clicked += OnAddClicked;

    }

    private void OnDeleteClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();

        itemDetailsSection.visible = false;
    }

    private void OnAddClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemID = 1000 + itemList.Count;
        newItem.itemName = "NEW ITEM";

        itemList.Add(newItem);
        itemListView.Rebuild();
    }

    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDetailList_SO");

        if(dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDetailList_SO)) as ItemDetailList_SO;
        }

        itemList = dataBase.itemDetailList;
        //������޷���������
        EditorUtility.SetDirty(dataBase);
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                e.Q<Label>("Name").text = itemList[i] == null ? "NO ITEM" : itemList[i].itemName;
            }
        };

        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;

        itemListView.onSelectionChange += OnListSelectionChanged;

        itemDetailsSection.visible = false;


    }

    private void OnListSelectionChanged(IEnumerable<object> selectedItem)
    {
        activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();

        itemDetailsSection.visible = true;

    }

    private void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint();

        //���б�����ֵ�ͻص���
        itemDetailsSection.Q<IntegerField>().value = activeItem.itemID;
        itemDetailsSection.Q<IntegerField>().RegisterValueChangedCallback(evt =>
        {
            activeItem.itemID = evt.newValue;
        });

        itemDetailsSection.Q<TextField>().value = activeItem.itemName;
        itemDetailsSection.Q<TextField>().RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });

        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        itemDetailsSection.Q<ObjectField>("ItemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;

            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        }
        );

        itemDetailsSection.Q<ObjectField>("ItemSprite").value = activeItem.itemOnWorldSprite;
        itemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = evt.newValue as Sprite;
        }
        );

        itemDetailsSection.Q<EnumField>("ItemType").Init(activeItem.itemType);
        itemDetailsSection.Q<EnumField>("ItemType").value = activeItem.itemType;
        itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });

        itemDetailsSection.Q<TextField>("Description").value = activeItem.itemDescription;
        itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("ItemUseRadius").value = activeItem.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanPickedup").value = activeItem.canPickedup;
        itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("Price").value = activeItem.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
        });

        itemDetailsSection.Q<Slider>("SellPercentage").value = activeItem.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
        });
    }
}