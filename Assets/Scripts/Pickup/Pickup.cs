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

        if (_ammoType == AmmoType.Pistol)
        {
            inventory.AddPistolAmmo(15);
        }
        else if (_ammoType == AmmoType.AutomaticRifle)
        {
            inventory.AddAutomaticRifleAmmo(30);
        }
        else if (_ammoType == AmmoType.Shotgun)
        {
            inventory.AddShotgunAmmo(2);
        }
        else if (_ammoType == AmmoType.HealthKit)
        {
            inventory.AddHealthKit();
        }

        Debug.Log($"{_ammoType} ammo collected");
        Destroy(gameObject);
    }
}
