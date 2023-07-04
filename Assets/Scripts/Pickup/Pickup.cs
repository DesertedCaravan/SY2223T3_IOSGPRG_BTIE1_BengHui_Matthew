using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    Pistol,
    AutomaticRifle,
    Shotgun,
    HealthKit
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private AmmoType _ammoType;

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
        Sound.Instance.PickupItem();
        Debug.Log($"{_ammoType} ammo collected");
        Destroy(gameObject);
    }
}
