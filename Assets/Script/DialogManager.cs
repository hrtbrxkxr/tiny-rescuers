using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Image actorImage;
    [SerializeField] TMP_Text actorName;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] int lettersPerSecond;
    
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    Action onDialogFinished;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public bool IsShowing {get; private set;}
    
    public static DialogManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialog(Message[] messages, Actor[] actors, Action onFinished=null)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        IsShowing = true;
        
        onDialogFinished = onFinished;

        OnShowDialog?.Invoke();
        dialogBox.SetActive(true);
        DisplayMessage();
        // if (isActive == true)
        // {
        //     dialogBox.SetActive(true);
        //     Debug.Log(messages.Length);
        // }
        // else
        // {
        //     dialogBox.SetActive(false);
        // }
        
        // StartCoroutine(TypeDialog(dialog.Lines[0]));
    }
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isActive == true)
        {
            NextMessage();
        }
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        dialogText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else 
        {
            // Debug.Log("End!");
            isActive = false;
            IsShowing = false;
            dialogBox.SetActive(false);
            onDialogFinished.Invoke();
            OnCloseDialog?.Invoke();
            
        }
    }
    // public IEnumerator TypeDialog(string line)
    // {
    //     dialogText.text = "";
    //     foreach (var letter in line.ToCharArray())
    //     {
    //         dialogText.text += letter;
    //         yield return new WaitForSeconds(1f / lettersPerSecond);
    //     }

    // }


}
