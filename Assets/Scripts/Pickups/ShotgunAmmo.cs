using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PickupItem();

        inventory.AddAmmo(3, 2);

        base.PickupItem(inventory);
    }
}