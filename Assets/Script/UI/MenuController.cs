using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    // public InventoryUI inventoryUI;
    // public GameController gameController;
    // InventoryUI inventoryScript;

//     List<TMP_Text> menuItems;

//     int selectedItem = 0;
    
    // private void Awake()
    // {
    //     menuItems = menu.GetComponentsInChildren<TMP_Text>().ToList();
    // }

    public GameObject pausedGameButton, bagButton, settingButton, mainMenuButton, closeButton;
    
    // private void Awake()
    // {
    //     gameController = GetComponent<GameController>();
    // }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
    
    public void OpenMenu()
    {
       if (!menu.activeInHierarchy)
       {
            menu.SetActive(true);
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(pausedGameButton);
       }
 
       else if (menu.activeInHierarchy)
       {
            menu.SetActive(false);
            Time.timeScale = 1f;
            // settingMenu.SetActive(false);
       }
    }

    public void OpenBag()
    {
        GameController.Instance.OpenMenuSelected();
        // inventoryUI.HandleUpdate();
    }

    // public void CloseBag()
    // {
    //     inventoryUI.gameObject.SetActive(false);
    // }


    


    // public void OpenSetting()
    // {
    //     settingMenu.SetActive(true);
    // }

    // public void OpenMainMenu()
    // {
    //     Debug.Log("Go to main menu");
    // }

    // public void CloseSetting()
    // {
    //     settingMenu.SetActive(false);
    // }

    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    // public void HandleUpdate()
    // {
    //     int prevSelection = selectedItem;

    //     if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         selectedItem++;
    //     }
            
    //     else if (Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         selectedItem--;
    //     }
        
    //     selectedItem = Mathf.Clamp(selectedItem, 0 , menuItems.Count - 1);

    //     if (prevSelection != selectedItem)
    //     {
    //         UpdateItemSelection();
    //     }
            
    // }

    // void UpdateItemSelection()
    // {
    //     for (int i = 0; i < menuItems.Count; i++)
    //     {
    //         if (i == selectedItem)
    //         {
    //             menuItems[i].color = GlobalSetting.i.HighlightedColor;
    //         }

    //         else
    //         {
    //             menuItems[i].color = Color.black;
    //         }
    //     }
    // }
}
