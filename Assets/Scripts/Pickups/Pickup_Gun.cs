using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Gun : Pickup
{
    public override void PickupItem(Inventory inventory, PickupType _pickupType)
    {
        if (_pickupType == PickupType.PistolGun)
        {
            Sound.Instance.PlayerGun(2);
            inventory.AddGun(1);
        }
        else if (_pickupType == PickupType.AutomaticRifleGun)
        {
            Sound.Instance.PlayerGun(4);
            inventory.AddGun(2);
        }
        else if (_pickupType == PickupType.ShotgunGun)
        {
            Sound.Instance.PlayerGun(6);
            inventory.AddGun(3);
        }

        base.PickupItem(inventory, _pickupType);
    }
}