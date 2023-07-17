using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifleGun : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PlayerGun(4);

        inventory.AddGun(2);

        base.PickupItem(inventory);
    }
}