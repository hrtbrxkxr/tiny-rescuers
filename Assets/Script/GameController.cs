using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public enum GameState {Walking, Talking, Paused, Menu, Bag}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;
    public InventoryUI inventoryUI;
    GameState state;
    GameState prevState;

    MenuController menuController;

    public static GameController Instance {get; private set;}

    private void Awake()
    {
        Instance = this;

        menuController = GetComponent<MenuController>();

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            prevState = state;
            state = GameState.Talking;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Talking)
            {    
                state = prevState;
            }
        };
    }

    public void OpenMenuSelected()
    {
        inventoryUI.gameObject.SetActive(true);
        state = GameState.Bag;
    }

    private void Update()
    {
        if (state == GameState.Walking)
        {
            playerController.HandleUpdate();

            // if (Input.GetKeyDown(KeyCode.Return))
            // {
            //     menuController.OpenMenu();
            //     state = GameState.Menu;
            // }
            
            // if (Input.GetKeyDown(KeyCode.S))
            // {
            //     SavingSystem.i.Save("saveSlot1");
            // }

            // if (Input.GetKeyDown(KeyCode.L))
            // {
            //     SavingSystem.i.Load("saveSlot1");
        }    // }
        
        else if (state == GameState.Talking)
        {
            DialogManager.Instance.HandleUpdate();
        }

        else if (state == GameState.Bag)
        {
            Action onBack = () =>
            {
                inventoryUI.gameObject.SetActive(false);
                state = GameState.Walking; 
            };

            inventoryUI.HandleUpdate(onBack);
        }

    }

    // public void HandleUpdate(Action onBack)
    // {
    //     Action onBack = () =>
    //     {
    //         inventoryUI.gameObject.SetActive(false);
    //         state = GameState.Walking; 
    //     };

    //     inventoryUI.HandleUpdate(onBack);
    // }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            prevState = state;
            state = GameState.Paused;
        }
        else
        {
            state = prevState;
        }
    }
}



