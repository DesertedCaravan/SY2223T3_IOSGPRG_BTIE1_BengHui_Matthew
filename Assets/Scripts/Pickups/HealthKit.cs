using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : Pickup
{
    public override void PickupItem(Inventory inventory)
    {
        Sound.Instance.PickupItem();

        inventory.AddHealthKit();

        base.PickupItem(inventory);
    }
}