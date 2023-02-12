using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemIcon;


    public string Name => itemName;
    public string Description => itemDescription;
    public Sprite Icon => itemIcon;

}
