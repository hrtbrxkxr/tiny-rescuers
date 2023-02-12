using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject pickupBox;
    // [SerializeField] Image actorImage;
    [SerializeField] TMP_Text actorName;
    [SerializeField] TMP_Text pickupText;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] Image npcPortrait;
    
    // Message[] currentMessages;
    // Actor[] currentActors;

    // Dialog dialog;

    // int activeMessage = 0;

    // int currentLine = 0;
    // bool isTyping;

    public static bool isActive = false;
    bool isPickup = false;
    // Action onDialogFinished;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public bool IsShowing {get; private set;}
    
    public static DialogManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialogText(string text, bool waitForInput=true, bool autoClose=true)
    {
        OnShowDialog?.Invoke();
        
        IsShowing = true;
        dialogBox.SetActive(true);

        yield return TypeDialog(text,isPickup);

        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        if (autoClose)
        {
            CloseDialog();
        }
    }
    public IEnumerator ShowPickupText(string text, bool waitForInput=true, bool autoClose=true)
    {
        OnShowDialog?.Invoke();
        
        IsShowing = true;
        pickupBox.SetActive(true);

        yield return TypeDialog(text,isPickup=true);

        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        if (autoClose)
        {
            ClosePickupDialog();
        }
    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();
    }

    public void ClosePickupDialog()
    {
        pickupBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();
    }
    
    public IEnumerator ShowDialog(Dialog dialog, Sprite portrait, string npcName)
    {
        // currentMessages = messages;
        // currentActors = actors;
        // activeMessage = 0;
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        
        isActive = true;
        IsShowing = true;
        
        dialogBox.SetActive(true);
        actorName.text = npcName;
        npcPortrait.sprite = portrait;

        foreach (var line in dialog.Lines)
        {
            yield return TypeDialog(line,isPickup=false);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();


        // yield return DisplayMessage();
        // if (isActive == true)
        // {
        //     dialogBox.SetActive(true);
        //     Debug.Log(messages.Length);
        // }
        // else
        // {
        //     dialogBox.SetActive(false);
        // }
        
    }
    public void HandleUpdate()
    {
        
    }

    // private IEnumerator DisplayMessage()
    // {
        
    //     Message messageToDisplay = currentMessages[activeMessage];
    //     dialogText.text = messageToDisplay.message;

    //     Actor actorToDisplay = currentActors[messageToDisplay.actorId];
    //     actorName.text = actorToDisplay.name;
    //     actorImage.sprite = actorToDisplay.sprite;
    //     yield return null;


    // }

    // public IEnumerator NextMessage()
    // {
    //     activeMessage++;
    //     if (activeMessage < currentMessages.Length)
    //     {
    //         yield return DisplayMessage();
    //     }
    //     else 
    //     {
    //         // Debug.Log("End!");
    //         isActive = false;
    //         IsShowing = false;
    //         dialogBox.SetActive(false);
    //         onDialogFinished?.Invoke();
    //         OnCloseDialog?.Invoke();
            
    //     }
    // }
    public IEnumerator TypeDialog(string line, bool isPickup)
    {
        if (isPickup == true)
        {
            pickupText.text = "";
            foreach (var letter in line.ToCharArray())
            {
                pickupText.text += letter;
                yield return new WaitForSeconds(1f/lettersPerSecond);
            }
        }

        else 
        {
            dialogText.text = "";
            foreach (var letter in line.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(1f/lettersPerSecond);
            }
        }
        
        
        
        // dialogText.text = "";
        // foreach (var letter in line.ToCharArray())
        // {
        //     dialogText.text += letter;
        //     yield return new WaitForSeconds(1f / lettersPerSecond);
        // }

    }


}
