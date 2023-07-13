using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifleAmmo : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PickupItem();

        inventory.AddAmmo(2, 30);

        base.PickupItem(inventory);
    }
}