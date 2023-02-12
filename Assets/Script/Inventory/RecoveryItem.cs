using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Create new recovery item")]

public class RecoveryItem : ItemBase
{
    [Header("HP")]
    [SerializeField] int hpAmount;
    [SerializeField] bool restoreMaxHP;

    int HP;
    int maxHP = 3;

    // public override bool Use()
    // {
    //     if (hpAmount > 0)
    //     {
    //         if (HP == maxHP)
    //         {
    //             return false;
    //         }
    //         else
    //         {
    //             //hihi
    //         }
    //     }
    //     return true;
    // }
}
