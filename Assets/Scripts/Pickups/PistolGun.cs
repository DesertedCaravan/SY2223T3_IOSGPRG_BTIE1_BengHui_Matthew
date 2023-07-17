using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGun : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PlayerGun(2);

        inventory.AddGun(1);

        base.PickupItem(inventory);
    }
}