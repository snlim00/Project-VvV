using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTurret : Item
{
    protected override void GiveItem()
    {
        PlayerController.S.TurretUpgrade();
    }
}
