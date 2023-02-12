using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup : MonoBehaviour, Interactable
{
    [SerializeField] ItemBase item;

    // [SerializeField] 

    public bool Used {get; set;} = false;

    public IEnumerator Interact(Transform initiator)
    {
        if (!Used)
        {
            initiator.GetComponent<Inventory>().AddItem(item);
            
            Used = true;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            yield return DialogManager.Instance.ShowPickupText($"Player found {item.Name}");

        }
       
    }
    
    
}
