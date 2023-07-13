using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunGun : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.ShotgunReload();

        inventory.AddGun(3);

        base.PickupItem(inventory);
    }
}