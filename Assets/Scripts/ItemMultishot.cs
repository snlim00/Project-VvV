using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMultishot : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.fireProp.Upgrade();
    }
}
