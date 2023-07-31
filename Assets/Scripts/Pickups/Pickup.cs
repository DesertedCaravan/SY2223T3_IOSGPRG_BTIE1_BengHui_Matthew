using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    PistolGun,
    AutomaticRifleGun,
    ShotgunGun,
    GrenadeLauncherGun,
    PistolAmmo,
    AutomaticRifleAmmo,
    ShotgunAmmo,
    GrenadeLauncherAmmo,
    HealthKit
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType _pickupType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Inventory inventory = collision.gameObject.GetComponent<Inventory>();

        if (inventory != null)
        {
            PickupItem(inventory, _pickupType);
        }
    }

    public virtual void PickupItem(Inventory inventory, PickupType _pickupType)
    {
        Debug.Log($"{_pickupType} collected");
        Destroy(gameObject);
    }
}
