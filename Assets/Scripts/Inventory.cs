using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Max Carry Capacity")]
    [SerializeField] private int _maxPistolAmmoCarry = 90;
    [SerializeField] private int _maxAutomaticRifleAmmoCarry = 120;
    [SerializeField] private int _maxShotgunAmmoCarry = 90;

    [SerializeField] private int _maxHealthKitCarry = 3;

    [Header("Equipped Gun")]
    [SerializeField] private Gun _equippedGun;

    [Header("Current Carry Capacity")]
    public int _pistolAmmoCarry = 0;
    public int _automaticRifleAmmoCarry = 0;
    public int _shotgunAmmoCarry = 0;

    public int _healthKitsCarry = 0;

    public void AddPistolAmmo(int amount)
    {
        _pistolAmmoCarry += amount;

        _pistolAmmoCarry = Mathf.Min(_pistolAmmoCarry, _maxPistolAmmoCarry);
    }

    public void AddGun()
    {

    }

    public void AddAutomaticRifleAmmo(int amount)
    {
        _automaticRifleAmmoCarry += amount;

        _automaticRifleAmmoCarry = Mathf.Min(_automaticRifleAmmoCarry, _maxAutomaticRifleAmmoCarry);
    }

    public void AddShotgunAmmo(int amount)
    {
        _shotgunAmmoCarry += amount;

        _shotgunAmmoCarry = Mathf.Min(_shotgunAmmoCarry, _maxShotgunAmmoCarry);
    }

    public void ReloadGun()
    {
        _equippedGun.Reload();
    }

    public void SwitchGuns()
    {
        // Switch from Primary to Secondary Gun

        // Check if there is a Primary equipped
        // Then check if there is a Secondary equipped
    }

    public void AddHealthKit()
    {
        if (_healthKitsCarry < _maxHealthKitCarry)
        {
            _healthKitsCarry++;
        }
    }

    public void UseHealthKit()
    {
        if (_healthKitsCarry > 0)
        {
            _healthKitsCarry--;
        }
    }
}
