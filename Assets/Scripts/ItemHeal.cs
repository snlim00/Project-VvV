using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.Heal(30);
    }
}
