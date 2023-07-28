using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_HealthKit : Pickup
{
    public override void PickupItem(Inventory inventory, PickupType _pickupType)
    {
        Sound.Instance.PickupItem();

        inventory.AddHealthKit();

        base.PickupItem(inventory, _pickupType);
    }
}