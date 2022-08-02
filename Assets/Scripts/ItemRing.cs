using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRing : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.ringCtrl.Upgrade();
    }
}
