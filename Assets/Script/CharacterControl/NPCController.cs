using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour, Interactable
{
    // public Message[] messages;
    // public Actor[] actors;
    [SerializeField] List<Sprite> sprites;

    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPattern;

    [SerializeField] Dialog dialog;
    public Sprite portrait;
    public string npcName;

    float idleTimer = 0f;
    NPCState state;
    int currentPattern = 0;

    Character character;
    ItemGiver itemGiver;

    private void Awake()
    {
        character = GetComponent<Character>();
        itemGiver = GetComponent<ItemGiver>();
    }
    
    public IEnumerator Interact(Transform initiator)
    {
        if (state == NPCState.Idle)   
        {
            state = NPCState.Dialog;
            character.LookTowards(initiator.position);

            if (itemGiver != null && itemGiver.CanBeGiven())
            {
                yield return itemGiver.GiveItem(initiator.GetComponent<PlayerController>());
            }

            else
            {
                yield return DialogManager.Instance.ShowDialog(dialog,portrait,npcName);
            }

            idleTimer = 0f;
            state = NPCState.Idle;
        }
        
    }

    private void Update()
    {

        if (state == NPCState.Idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;
                if(movementPattern.Count > 0)
                {
                    StartCoroutine(Walk());
                }    
            }
        }
        character.HandleUpdate();
    }

    IEnumerator Walk()
    {
        state = NPCState.Walking;

        var oldPos = transform.position;

        yield return character.Move(movementPattern[currentPattern]);
        
        if(transform.position != oldPos)
            currentPattern = (currentPattern + 1) % movementPattern.Count;
        
        state = NPCState.Idle;
    }
}


[System.Serializable]

public class Message
{
    public int actorId;
    public string message;
}
[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

public enum NPCState {Idle, Walking, Dialog}
