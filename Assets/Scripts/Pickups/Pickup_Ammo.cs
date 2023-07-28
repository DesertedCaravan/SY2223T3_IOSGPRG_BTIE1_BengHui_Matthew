using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Ammo : Pickup
{
    public override void PickupItem(Inventory inventory, PickupType _pickupType)
    {
        Sound.Instance.PickupItem();

        int amount = 0;

        if (_pickupType == PickupType.PistolAmmo)
        {
            amount = Random.Range(1, 9);
            inventory.AddAmmo(1, amount);
        }
        else if (_pickupType == PickupType.AutomaticRifleAmmo)
        {
            amount = Random.Range(5, 16);
            inventory.AddAmmo(2, amount);
        }
        else if (_pickupType == PickupType.ShotgunAmmo)
        {
            amount = Random.Range(1, 3);
            inventory.AddAmmo(3, amount);
        }

        base.PickupItem(inventory, _pickupType);
    }
}