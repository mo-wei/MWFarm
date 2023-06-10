using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWFarm.Inventory;
public class ItemManager : Singleton<ItemManager>
{
    [Header("������ƷԤ�Ƽ�")]
    public Item itemPrefabe;

    private Transform itemParent;//������Ʒ�ĸ�����


    private void Start()
    {
        itemParent = GameObject.FindWithTag("ItemParent").transform;
    }
    private void OnEnable()
    {
        EventHandler.InstantiateItemInscene += OnInstantiateItemInscene;
    }
    private void OnDisable()
    {
        EventHandler.InstantiateItemInscene -= OnInstantiateItemInscene;
    }

    private void OnInstantiateItemInscene(int ID, Vector3 postion)
    {
        var item = Instantiate(itemPrefabe, postion, Quaternion.identity, itemParent);
        item.itemID = ID;
    }
}
