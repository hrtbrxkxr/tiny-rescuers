using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    [SerializeField] ItemBase item;
    [SerializeField] Dialog dialog;

    bool used = false;

    NPCController npcController;

    public void Awake()
    {
        npcController = GetComponent<NPCController>();
    }

    public IEnumerator GiveItem(PlayerController player)
    {
        yield return DialogManager.Instance.ShowDialog(dialog,npcController.portrait,npcController.npcName);
        
        player.GetComponent<Inventory>().AddItem(item);

        used = true;
        yield return DialogManager.Instance.ShowPickupText($"You recieved {item.name}");
    }

    public bool CanBeGiven()
    {
        return item != null && !used;
    }
}
