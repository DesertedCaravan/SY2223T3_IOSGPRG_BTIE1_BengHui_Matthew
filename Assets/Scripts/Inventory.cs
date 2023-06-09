using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Max Carry Capacity")]
    [SerializeField] private int _maxPistolAmmoCarry = 90;
    [SerializeField] private int _maxAutomaticRifleAmmoCarry = 120;
    [SerializeField] private int _maxShotgunAmmoCarry = 90;

    [SerializeField] private int _maxPistolClip = 15;
    [SerializeField] private int _maxAutomaticRifleClip = 30;
    [SerializeField] private int _maxShotgunClip = 2;

    [SerializeField] private int _maxHealthKitCarry = 3;

    [Header("Equipped Gun")]
    [SerializeField] private Gun _equippedGun;
    [SerializeField] private Gun _primaryGun;
    [SerializeField] private Gun _secondaryGun;

    public Gun _pistolSprite;
    public Gun _automaticRifleSprite;
    public Gun _shotgunSprite;

    [Header("Current Clip")]
    public int _pistolClip = 0;
    public int _automaticRifleClip = 0;
    public int _shotgunClip = 0;

    [Header("Current Unused Ammo")]
    public int _pistolAmmoExcess = 0;
    public int _automaticRifleAmmoExcess = 0;
    public int _shotgunAmmoExcess = 0;

    [Header("Total Carry Capacity")]
    public int _pistolAmmoCarry = 0;
    public int _automaticRifleAmmoCarry = 0;
    public int _shotgunAmmoCarry = 0;

    public int _healthKitsCarry = 0;

    public void AddAmmo(int gunType, int amount)
    {
        if (gunType == 1)
        {
            _pistolAmmoExcess += amount;
            _pistolAmmoExcess = Mathf.Min(_pistolAmmoExcess, _maxPistolAmmoCarry);
            
            if (_pistolClip == 0)
            {
                _pistolClip += _maxPistolClip;
                _pistolAmmoExcess -= _maxPistolClip;

                GameManager.Instance.SetClip(1, _pistolClip);
            }

            GameManager.Instance.SetExcess(1, _pistolAmmoExcess);

            if (_equippedGun == _pistolSprite)
            {
                GameManager.Instance.SetCurrentClip(1);
                GameManager.Instance.SetCurrentExcess(1);
            }

            _pistolAmmoCarry += amount;
            _pistolAmmoCarry = Mathf.Min(_pistolAmmoCarry, _maxPistolAmmoCarry);

            GameManager.Instance.SetAmmo(1, _pistolAmmoCarry);
        }
        else if (gunType == 2)
        {
            _automaticRifleAmmoExcess += amount;
            _automaticRifleAmmoExcess = Mathf.Min(_automaticRifleAmmoExcess, _maxAutomaticRifleAmmoCarry);

            if (_automaticRifleClip == 0)
            {
                _automaticRifleClip += _maxAutomaticRifleClip;
                _automaticRifleAmmoExcess -= _maxAutomaticRifleClip;

                GameManager.Instance.SetClip(2, _automaticRifleClip);
            }

            GameManager.Instance.SetExcess(2, _automaticRifleAmmoExcess);

            if (_equippedGun == _automaticRifleSprite)
            {
                GameManager.Instance.SetCurrentClip(2);
                GameManager.Instance.SetCurrentExcess(2);
            }

            _automaticRifleAmmoCarry += amount;
            _automaticRifleAmmoCarry = Mathf.Min(_automaticRifleAmmoCarry, _maxAutomaticRifleAmmoCarry);

            GameManager.Instance.SetAmmo(2, _automaticRifleAmmoCarry);
        }
        else if (gunType == 3)
        {
            _shotgunAmmoExcess += amount;
            _shotgunAmmoExcess = Mathf.Min(_shotgunAmmoExcess, _maxShotgunAmmoCarry);

            if (_shotgunClip == 0)
            {
                _shotgunClip += _maxShotgunClip;
                _shotgunAmmoExcess -= _maxShotgunClip;

                GameManager.Instance.SetClip(3, _shotgunClip);
            }

            GameManager.Instance.SetExcess(3, _shotgunAmmoExcess);

            if (_equippedGun == _shotgunSprite)
            {
                GameManager.Instance.SetCurrentClip(3);
                GameManager.Instance.SetCurrentExcess(3);
            }

            _shotgunAmmoCarry += amount;
            _shotgunAmmoCarry = Mathf.Min(_shotgunAmmoCarry, _maxShotgunAmmoCarry);

            GameManager.Instance.SetAmmo(3, _shotgunAmmoCarry);
        }
    }

    public bool CheckClip(int gunType)
    {
        if (gunType == 1 && _pistolClip <= 0)
        {
            return false;
        }
        else if (gunType == 2 && _automaticRifleClip <= 0)
        {
            return false;
        }
        else if (gunType == 3 && _shotgunClip <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckAmmo(int gunType)
    {
        if (gunType == 1 && _pistolAmmoCarry <= 0)
        {
            return false;
        }
        else if (gunType == 2 && _automaticRifleAmmoCarry <= 0)
        {
            return false;
        }
        else if (gunType == 3 && _shotgunAmmoCarry <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UseAmmo(int gunType, int amount)
    {
        if (gunType == 1)
        {
            // Reduce Clip
            _pistolClip -= amount;
            _pistolClip = Mathf.Max(0, _pistolClip);

            // Reduce Total Ammo Carry
            _pistolAmmoCarry -= amount;
            _pistolAmmoCarry = Mathf.Max(0, _pistolAmmoCarry);

            // Establish Clip in UI
            GameManager.Instance.SetClip(1, _pistolClip);
            GameManager.Instance.SetCurrentClip(1);

            GameManager.Instance.SetAmmo(1, _pistolAmmoCarry);
        }
        else if (gunType == 2)
        {
            _automaticRifleClip -= amount;
            _automaticRifleClip = Mathf.Max(0, _automaticRifleClip);

            _automaticRifleAmmoCarry -= amount;
            _automaticRifleAmmoCarry = Mathf.Max(0, _automaticRifleAmmoCarry);

            GameManager.Instance.SetClip(2, _automaticRifleClip);
            GameManager.Instance.SetCurrentClip(2);

            GameManager.Instance.SetAmmo(2, _automaticRifleAmmoCarry);
        }
        else if (gunType == 3)
        {
            _shotgunClip -= amount;
            _shotgunClip = Mathf.Max(0, _shotgunClip);

            _shotgunAmmoCarry -= amount;
            _shotgunAmmoCarry = Mathf.Max(0, _shotgunAmmoCarry);

            GameManager.Instance.SetClip(3, _shotgunClip);
            GameManager.Instance.SetCurrentClip(3);

            GameManager.Instance.SetAmmo(3, _shotgunAmmoCarry);
        }
    }

    public int CheckGun()
    {
        if (_equippedGun == _pistolSprite)
        {
            return 1;
        }
        else if (_equippedGun == _automaticRifleSprite)
        {
            return 2;
        }
        else if (_equippedGun == _shotgunSprite)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    public void AddGun(int gunType)
    {
        if (gunType == 1)
        {
            _equippedGun = _pistolSprite;
            _secondaryGun = _pistolSprite;

            // Establish Clip and Total Ammo in UI
            GameManager.Instance.SetCurrentClip(1);

            GameManager.Instance.SetExcess(1, _pistolAmmoExcess);
            GameManager.Instance.SetCurrentExcess(1);
        }
        else if (gunType == 2)
        {
            _equippedGun = _automaticRifleSprite;
            _primaryGun = _automaticRifleSprite;

            GameManager.Instance.SetCurrentClip(2);

            GameManager.Instance.SetExcess(2, _automaticRifleAmmoExcess);
            GameManager.Instance.SetCurrentExcess(2);
        }
        else if (gunType == 3)
        {
            _equippedGun = _shotgunSprite;
            _primaryGun = _shotgunSprite;

            GameManager.Instance.SetCurrentClip(3);

            GameManager.Instance.SetExcess(3, _shotgunAmmoExcess);
            GameManager.Instance.SetCurrentExcess(3);
        }

        UIManager.Instance.ActivateGun(gunType, _pistolSprite, _automaticRifleSprite, _shotgunSprite);
    }

    public void ReloadGun(int gunType)
    {
        if (gunType == 1 && _pistolAmmoCarry > 0)
        {
            // Reduce Excess
            if (_pistolAmmoCarry < _maxPistolClip)
            {
                _pistolAmmoExcess -= _pistolAmmoCarry;
                _pistolClip += _pistolAmmoCarry;
            }
            else
            {
                _pistolAmmoExcess -= _maxPistolClip;
                _pistolClip += _maxPistolClip;
            }

            _pistolAmmoExcess = Mathf.Max(0, _pistolAmmoExcess);

            GameManager.Instance.SetExcess(1, _pistolAmmoExcess);
            GameManager.Instance.SetCurrentExcess(1);

            // Establish Clip in UI
            GameManager.Instance.SetClip(1, _pistolClip);
            GameManager.Instance.SetCurrentClip(1);

            // Reduce Total Ammo Carry
            _pistolAmmoCarry -= _maxPistolClip;
            _pistolAmmoCarry = Mathf.Max(0, _pistolAmmoCarry);

            GameManager.Instance.SetAmmo(1, _pistolAmmoCarry);

            Sound.Instance.PistolReload();
        }
        else if (gunType == 2 && _automaticRifleAmmoCarry > 0)
        {
            if (_automaticRifleAmmoCarry < _maxAutomaticRifleClip)
            {
                _automaticRifleAmmoExcess -= _automaticRifleAmmoCarry;
                _automaticRifleClip += _automaticRifleAmmoCarry;
            }
            else
            {
                _automaticRifleAmmoExcess -= _maxAutomaticRifleClip;
                _automaticRifleClip += _maxAutomaticRifleClip;
            }

            _automaticRifleAmmoExcess = Mathf.Max(0, _automaticRifleAmmoExcess);

            GameManager.Instance.SetExcess(2, _automaticRifleAmmoExcess);
            GameManager.Instance.SetCurrentExcess(2);

            GameManager.Instance.SetClip(2, _automaticRifleClip);
            GameManager.Instance.SetCurrentClip(2);

            _automaticRifleAmmoCarry -= _maxAutomaticRifleClip;
            _automaticRifleAmmoCarry = Mathf.Max(0, _automaticRifleAmmoCarry);

            GameManager.Instance.SetAmmo(2, _automaticRifleAmmoCarry);

            Sound.Instance.AutomaticRifleReload();
        }
        else if (gunType == 3 && _shotgunAmmoCarry > 0)
        {
            if (_shotgunAmmoCarry < _maxShotgunClip)
            {
                _shotgunAmmoExcess -= _shotgunAmmoCarry;
                _shotgunClip += _shotgunAmmoCarry;
            }
            else
            {
                _shotgunAmmoExcess -= _maxShotgunClip;
                _shotgunClip += _maxShotgunClip;
            }

            _shotgunAmmoExcess = Mathf.Max(0, _shotgunAmmoExcess);

            GameManager.Instance.SetExcess(3, _shotgunAmmoExcess);
            GameManager.Instance.SetCurrentExcess(3);

            GameManager.Instance.SetClip(3, _shotgunClip);
            GameManager.Instance.SetCurrentClip(3);

            _shotgunAmmoCarry -= _maxShotgunClip;
            _shotgunAmmoCarry = Mathf.Max(0, _shotgunAmmoCarry);

            GameManager.Instance.SetAmmo(3, _shotgunAmmoCarry);

            Sound.Instance.ShotgunReload();
        }
    }

    public void SwitchGuns(int arm)
    {
        if ((arm == 1) && _primaryGun != null && _secondaryGun != null)
        {
            _equippedGun = _primaryGun;

            if (_equippedGun == _automaticRifleSprite)
            {
                UIManager.Instance.ActivateGun(2, _pistolSprite, _automaticRifleSprite, _shotgunSprite);
                GameManager.Instance.SetCurrentClip(2);
                GameManager.Instance.SetCurrentExcess(2);

                Sound.Instance.AutomaticRifleReload();
            }
            else
            {
                UIManager.Instance.ActivateGun(3, _pistolSprite, _automaticRifleSprite, _shotgunSprite);
                GameManager.Instance.SetCurrentClip(3);
                GameManager.Instance.SetCurrentExcess(3);

                Sound.Instance.ShotgunReload();
            }
        }
        else if ((arm == 2) && _primaryGun != null && _secondaryGun != null)
        {
            if (_equippedGun == _primaryGun)
            {
                _equippedGun = _secondaryGun;

                UIManager.Instance.ActivateGun(1, _pistolSprite, _automaticRifleSprite, _shotgunSprite);

                // Establish Clip and Total Ammo in UI
                GameManager.Instance.SetCurrentClip(1);
                GameManager.Instance.SetCurrentExcess(1);

                Sound.Instance.PistolReload();
            }
        }
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

            Debug.Log($"{_healthKitsCarry} left.") ;
        }
        else
        {
            Debug.Log("Empty");
        }
    }
}
