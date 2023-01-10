using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Walking, Talking, Paused}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    [SerializeField] Camera worldCamera;
    GameState state;
    GameState stateBeforePause;

    public static GameController Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Talking;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Talking)
            {    
                state = GameState.Walking;
            }
        };
    }
    private void Update()
    {
        if (state == GameState.Walking)
        {
            PlayerController.HandleUpdate();
        }
        else if (state == GameState.Talking)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }
}
