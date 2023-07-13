using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    PistolGun,
    AutomaticRifleGun,
    ShotgunGun,
    PistolAmmo,
    AutomaticRifleAmmo,
    ShotgunAmmo,
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
            PickupItem(inventory);
        }
    }

    public virtual void PickupItem(Inventory inventory)
    {
        Debug.Log($"{_pickupType} collected");
        Destroy(gameObject);
    }
}
