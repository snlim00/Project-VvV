using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPpHeal : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.PpHeal(20);
    }
}
