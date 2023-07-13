using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PickupItem();

        inventory.AddAmmo(1, 15);

        base.PickupItem(inventory);
    }
}