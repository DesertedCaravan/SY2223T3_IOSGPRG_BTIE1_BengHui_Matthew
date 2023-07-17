using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunGun : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PlayerGun(6);

        inventory.AddGun(3);

        base.PickupItem(inventory);
    }
}