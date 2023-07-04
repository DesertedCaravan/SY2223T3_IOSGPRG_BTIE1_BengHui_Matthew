using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        inventory.AddHealthKit();

        base.PickupItem(inventory);
    }
}