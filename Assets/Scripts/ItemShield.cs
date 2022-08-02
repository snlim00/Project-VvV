using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.StartCoroutine(PlayerController.S.Shield());
    }
}
