using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGun : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PistolReload();

        inventory.AddGun(1);

        base.PickupItem(inventory);
    }
}