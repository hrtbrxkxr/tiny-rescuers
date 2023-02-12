using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum InventoryUIState {ItemSelection, Busy}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI categoryText;

    Inventory inventory;

    RectTransform itemListRect;

    InventoryUIState state;

    int selectedItem = 0;
    int selectedCategory = 0;

    const int itemsInViewPoint = 8;

    List<ItemSlotUI> slotUIList;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
        itemListRect = itemList.GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateItemList();

        inventory.OnUpdated += UpdateItemList;
    }

    void UpdateItemList()
    {
        //Clear all item
        foreach (Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }

        slotUIList = new List<ItemSlotUI>();

        foreach (var itemSlot in inventory.GetSlotsByCategory(selectedCategory))
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);

            slotUIList.Add(slotUIObj);
        }
    }

    public void HandleUpdate(Action onBack)
    {
        int prevSelection = selectedItem;
        int prevCategory = selectedCategory;
        

        // if (state == InventoryUIState.ItemSelection)
        // {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedItem++;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedItem--;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedCategory++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedCategory--;
            }

            if (selectedCategory > Inventory.ItemCategories.Count - 1)
            {
                selectedCategory = 0;
            }

            else if (selectedCategory < 0)
            {
                selectedCategory = Inventory.ItemCategories.Count - 1;
            }

            selectedItem = Mathf.Clamp(selectedItem, 0 , inventory.GetSlotsByCategory(selectedCategory).Count - 1);

            if (prevCategory != selectedCategory)
            {
                ResetSelection();
                categoryText.text = Inventory.ItemCategories[selectedCategory];
                UpdateItemList();
            }
            
            else if (prevSelection != selectedItem)
            {
                UpdateItemSelection();
            }

            
            if (Input.GetKeyDown(KeyCode.X))
            {
                onBack?.Invoke();
            }

        //}
        
        
    }

    void UpdateItemSelection()
    {
        var slots = inventory.GetSlotsByCategory(selectedCategory);
        for (int i = 0; i < slotUIList.Count ; i++)
        {
            if (i == selectedItem)
            {
                slotUIList[i].NameText.color = GlobalSetting.i.HighlightedColor;
                slotUIList[i].CountText.color = GlobalSetting.i.HighlightedColor;
            }
                
            else
            {
                slotUIList[i].NameText.color = GlobalSetting.i.DefaultColor;
                slotUIList[i].CountText.color = GlobalSetting.i.DefaultColor;
            }
                
        }

        if (inventory.Slots.Count > 0)
        {
            var item = slots[selectedItem].Item;
            itemIcon.sprite = item.Icon;
            itemDescription.text = item.Description;
            itemNameText.text = item.Name;
        }

       

        HandleScrolling();
    }

    void HandleScrolling()
    {
        if (slotUIList.Count <= itemsInViewPoint) return;
        
        float scrollPos = Mathf.Clamp(selectedItem - itemsInViewPoint/2, 0 ,selectedItem) * slotUIList[0].Height;
        itemListRect.localPosition = new Vector2(itemListRect.localPosition.x, scrollPos);
        
    }

    void ResetSelection()
    {
        selectedItem = 0;

        itemIcon.sprite = null;
        itemDescription.text = "";
        itemNameText.text = "";
    }

    // IEnumerator UseItem()
    // {
    //     // state = InventoryUIState.Busy;
    // }



}
