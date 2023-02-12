using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum ItemCategory {Items, QItems}

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> slots;
    [SerializeField] List<ItemSlot> questItemSlots;

    public List<ItemSlot> Slots => slots;

    List<List<ItemSlot>> allSlots;

    private void Awake()
    {
        allSlots = new List<List<ItemSlot>>() { slots, questItemSlots };
    }

    public static List<string> ItemCategories {get; set;} = new List<string>()
    {
        "ITEMS", "QUEST ITEMS"
    };

    public event Action OnUpdated;

    public List<ItemSlot> GetSlotsByCategory(int categoryIndex)
    {
        return allSlots[categoryIndex];
    }

    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    // public ItemBase UseItem(int itemIndex)
    // {
    //     var item = slots[itemIndex].Item;
    //     //bool itemUsed = item.Use()
    // }

    // public void RemoveItem(ItemBase item)
    // {
    //     var itemSlot = slots.First(slot => slot.item == item);
    //     itemSlot.Count--;
    //     if (itemSlot.Count == 0)
    //     {
    //         slots.Remove(itemSlot);
    //     }
    //     OnUpdated?.Invoke();
    // }

    public void GetItem()
    {

    }

    ItemCategory GetCategoryFromItem(ItemBase item)
    {
        if (item is RecoveryItem)
            return ItemCategory.Items;
        else 
            return ItemCategory.QItems;
    }

    public void AddItem(ItemBase item, int count=1) 
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = currentSlots.FirstOrDefault(slot => slot.Item == item);
        
        if (itemSlot != null)
        {
            itemSlot.Count += count;
        }
        else
        {
            currentSlots.Add(new ItemSlot()
            {
                Item = item,
                Count = count
            });    
        }
        
        OnUpdated?.Invoke();
    }
}

[Serializable]

public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemBase Item {
        get => item;
        set => item = value;
    } 
    public int Count{
        get => count;
        set => count = value;
    } 
}