using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Max Carry Capacity")]
    [SerializeField] private int _maxPistolAmmoCarry = 90;
    [SerializeField] private int _maxAutomaticRifleAmmoCarry = 120;
    [SerializeField] private int _maxShotgunAmmoCarry = 90;
    [SerializeField] private int _maxGrenadeLauncherAmmoCarry = 10;

    [SerializeField] private int _maxPistolClip = 15;
    [SerializeField] private int _maxAutomaticRifleClip = 30;
    [SerializeField] private int _maxShotgunClip = 2;
    [SerializeField] private int _maxGrenadeLauncherClip = 1;

    [SerializeField] private int _maxHealthKitCarry = 3;

    [Header("Equipped Gun")]
    [SerializeField] private Gun _equippedGun;
    [SerializeField] private Gun _primaryGun;
    [SerializeField] private Gun _secondaryGun;

    public Gun _pistolSprite;
    public Gun _automaticRifleSprite;
    public Gun _shotgunSprite;
    public Gun _grenadeLauncherSprite;

    public int _currentGun = 0;

    [Header("Current Clip")]
    public int _pistolClip = 0;
    public int _automaticRifleClip = 0;
    public int _shotgunClip = 0;
    public int _grenadeLauncherClip = 0;

    public bool _reloadState = false;
    public float _reloadTime = 3f;
    public float _currentReloadTime = 0f;

    [Header("Current Unused Ammo")]
    public int _pistolAmmoExcess = 0;
    public int _automaticRifleAmmoExcess = 0;
    public int _shotgunAmmoExcess = 0;
    public int _grenadeLauncherAmmoExcess = 0;

    [Header("Total Carry Capacity")]
    public int _pistolAmmoCarry = 0;
    public int _automaticRifleAmmoCarry = 0;
    public int _shotgunAmmoCarry = 0;
    public int _grenadeLauncherAmmoCarry = 0;

    public int _healthKitsCarry = 0;

    public void Update()
    {
        if (_reloadState == true)
        {
            GameManager.Instance.Reloading();

            _currentReloadTime -= 1f * Time.deltaTime;

            if (_currentReloadTime <= 0f)
            {
                if (_equippedGun == _pistolSprite)
                {
                    ReloadGun(1);
                }
                else if (_equippedGun == _automaticRifleSprite)
                {
                    ReloadGun(2);
                }
                else if (_equippedGun == _shotgunSprite)
                {
                    ReloadGun(3);
                }
                else if (_equippedGun == _grenadeLauncherSprite)
                {
                    ReloadGun(4);
                }
            }
        }
    }

    public void AddAmmo(int gunType, int amount)
    {
        int _clip = 0;
        int _maxClip = 0;
        int _ammoExcess = 0;
        int _ammoCarry = 0;
        int _maxAmmoCarry = 0;
        int _gun = 0;
        Gun _sprite = _pistolSprite;

        if (gunType == 1)
        {
            _clip = _pistolClip;
            _maxClip = _maxPistolClip;
            _ammoExcess = _pistolAmmoExcess;
            _ammoCarry = _pistolAmmoCarry;
            _maxAmmoCarry = _maxPistolAmmoCarry;
            _gun = 1;
            _sprite = _pistolSprite;
        }
        else if (gunType == 2)
        {
            _clip = _automaticRifleClip;
            _maxClip = _maxAutomaticRifleClip;
            _ammoExcess = _automaticRifleAmmoExcess;
            _ammoCarry = _automaticRifleAmmoCarry;
            _maxAmmoCarry = _maxAutomaticRifleAmmoCarry;
            _gun = 2;
            _sprite = _automaticRifleSprite;
        }
        else if (gunType == 3)
        {
            _clip = _shotgunClip;
            _maxClip = _maxShotgunClip;
            _ammoExcess = _shotgunAmmoExcess;
            _ammoCarry = _shotgunAmmoCarry;
            _maxAmmoCarry = _maxShotgunAmmoCarry;
            _gun = 3;
            _sprite = _shotgunSprite;
        }
        else if (gunType == 4)
        {
            _clip = _grenadeLauncherClip;
            _maxClip = _maxGrenadeLauncherClip;
            _ammoExcess = _grenadeLauncherAmmoExcess;
            _ammoCarry = _grenadeLauncherAmmoCarry;
            _maxAmmoCarry = _maxGrenadeLauncherAmmoCarry;
            _gun = 4;
            _sprite = _grenadeLauncherSprite;
        }

        _ammoExcess += amount;
        _ammoExcess = Mathf.Min(_ammoExcess, _maxAmmoCarry - _clip);

        if (_clip == 0)
        {
            if (_ammoExcess < _maxClip)
            {
                _clip += _ammoExcess;
                _ammoExcess -= _ammoExcess;
            }
            else
            {
                _clip += _maxClip;
                _ammoExcess -= _maxClip;
            }

            if (gunType == 1)
            {
                _pistolClip = _clip;
            }
            else if (gunType == 2)
            {
                _automaticRifleClip = _clip;
            }
            else if (gunType == 3)
            {
                _shotgunClip = _clip;
            }
            else if (gunType == 4)
            {
                _grenadeLauncherClip = _clip;
            }

            GameManager.Instance.SetClip(_gun, _clip);
        }

        if (gunType == 1)
        {
            _pistolAmmoExcess = _ammoExcess;
        }
        else if (gunType == 2)
        {
            _automaticRifleAmmoExcess = _ammoExcess;
        }
        else if (gunType == 3)
        {
            _shotgunAmmoExcess = _ammoExcess;
        }
        else if (gunType == 4)
        {
            _grenadeLauncherAmmoExcess = _ammoExcess;
        }

        GameManager.Instance.SetExcess(_gun, _ammoExcess);

        if (_equippedGun == _sprite)
        {
            GameManager.Instance.SetCurrentClip(_gun);
            GameManager.Instance.SetCurrentExcess(_gun);
        }

        if (_ammoCarry <= _maxAmmoCarry - _clip)
        {
            _ammoCarry += amount;
            _ammoCarry = Mathf.Min(_ammoCarry, _maxAmmoCarry);
        }
        else
        {
            _ammoCarry += amount;
            _ammoCarry = Mathf.Min(_ammoCarry, _maxAmmoCarry);
            Sound.Instance.FullAmmo();
        }

        if (gunType == 1)
        {
            _pistolAmmoCarry = _ammoCarry;
        }
        else if (gunType == 2)
        {
            _automaticRifleAmmoCarry = _ammoCarry;
        }
        else if (gunType == 3)
        {
            _shotgunAmmoCarry = _ammoCarry;
        }
        else if (gunType == 4)
        {
            _grenadeLauncherAmmoCarry = _ammoCarry;
        }

        GameManager.Instance.SetAmmo(_gun, _ammoCarry);
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
        else if (gunType == 4 && _grenadeLauncherClip <= 0)
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
        else if (gunType == 4 && _grenadeLauncherAmmoCarry <= 0)
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
        int _gun = 0;
        int _clip = 0;
        int _ammoCarry = 0;

        if (gunType == 1)
        {
            _gun = 1;
            _clip = _pistolClip;
            _ammoCarry = _pistolAmmoCarry;
        }
        else if (gunType == 2)
        {
            _gun = 2;
            _clip = _automaticRifleClip;
            _ammoCarry = _automaticRifleAmmoCarry;
        }
        else if (gunType == 3)
        {
            _gun = 3;
            _clip = _shotgunClip;
            _ammoCarry = _shotgunAmmoCarry;
        }
        else if (gunType == 4)
        {
            _gun = 4;
            _clip = _grenadeLauncherClip;
            _ammoCarry = _grenadeLauncherAmmoCarry;
        }

        _clip -= amount;
        _clip = Mathf.Max(0, _clip);

        _ammoCarry -= amount;
        _ammoCarry = Mathf.Max(0, _ammoCarry);

        if (gunType == 1)
        {
            _pistolClip = _clip;
            _pistolAmmoCarry = _ammoCarry;
        }
        else if (gunType == 2)
        {
            _automaticRifleClip = _clip;
            _automaticRifleAmmoCarry = _ammoCarry;
        }
        else if (gunType == 3)
        {
            _shotgunClip = _clip;
            _shotgunAmmoCarry = _ammoCarry;
        }
        else if (gunType == 4)
        {
            _grenadeLauncherClip = _clip;
            _grenadeLauncherAmmoCarry = _ammoCarry;
        }

        // Establish Clip in UI
        GameManager.Instance.SetClip(_gun, _clip);
        GameManager.Instance.SetCurrentClip(_gun);

        GameManager.Instance.SetAmmo(_gun, _ammoCarry);
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
        else if (_equippedGun == _grenadeLauncherSprite)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    public void AddGun(int gunType)
    {
        Gun _sprite = _pistolSprite;
        int _ammoExcess = 0;
        int _gun = 0;

        if (gunType == 1)
        {
            _sprite = _pistolSprite;
            _ammoExcess = _pistolAmmoExcess;
            _gun = 1;
        }
        else if (gunType == 2)
        {
            _sprite = _automaticRifleSprite;
            _ammoExcess = _automaticRifleAmmoExcess;
            _gun = 2;
        }
        else if (gunType == 3)
        {
            _sprite = _shotgunSprite;
            _ammoExcess = _shotgunAmmoExcess;
            _gun = 3;
        }
        else if (gunType == 4)
        {
            _sprite = _grenadeLauncherSprite;
            _ammoExcess = _grenadeLauncherAmmoExcess;
            _gun = 4;
        }

        _equippedGun = _sprite;

        if (gunType == 1)
        {
            _secondaryGun = _sprite;
        }
        else if (gunType == 2 || gunType == 3 || gunType == 4)
        {
            _primaryGun = _sprite;
        }

        GameManager.Instance.SetCurrentClip(_gun);

        GameManager.Instance.SetExcess(_gun, _ammoExcess);
        GameManager.Instance.SetCurrentExcess(_gun);

        UIManager.Instance.ActivateGun(gunType, _pistolSprite, _automaticRifleSprite, _shotgunSprite, _grenadeLauncherSprite);
        UIManager.Instance.ActivateGunSprite(gunType);
    }

    public void SetReloadTime(int gunType)
    {
        if (gunType == 1)
        {
            _reloadTime = 2f;
        }
        else if (gunType == 2)
        {
            _reloadTime = 2.3f;
        }
        else if (gunType == 3)
        {
            _reloadTime = 2.7f;
        }
        else if (gunType == 4)
        {
            _reloadTime = 2.3f;
        }

        _currentReloadTime = _reloadTime;
    }

    public void ReloadGun(int gunType)
    {
        if (_reloadState == false)
        {
            SetReloadTime(gunType);
            _reloadState = true;
        }

        if (_currentReloadTime <= 0f)
        {
            int _clip = 0;
            int _ammoExcess = 0;
            int _ammoCarry = 0;
            int _maxClip = 0;
            int _gun = 0;
            int _sound = 0;

            GameManager.Instance.ReloadingComplete();

            if (gunType == 1 && _pistolAmmoCarry > 0)
            {
                _clip = _pistolClip;
                _ammoExcess = _pistolAmmoExcess;
                _ammoCarry = _pistolAmmoCarry;
                _maxClip = _maxPistolClip;
                _gun = 1;
                _sound = 2;
            }
            else if (gunType == 2 && _automaticRifleAmmoCarry > 0)
            {
                _clip = _automaticRifleClip;
                _ammoExcess = _automaticRifleAmmoExcess;
                _ammoCarry = _automaticRifleAmmoCarry;
                _maxClip = _maxAutomaticRifleClip;
                _gun = 2;
                _sound = 4;
            }
            else if (gunType == 3 && _shotgunAmmoCarry > 0)
            {
                _clip = _shotgunClip;
                _ammoExcess = _shotgunAmmoExcess;
                _ammoCarry = _shotgunAmmoCarry;
                _maxClip = _maxShotgunClip;
                _gun = 3;
                _sound = 6;
            }
            else if (gunType == 4 && _grenadeLauncherAmmoCarry > 0)
            {
                _clip = _grenadeLauncherClip;
                _ammoExcess = _grenadeLauncherAmmoExcess;
                _ammoCarry = _grenadeLauncherAmmoCarry;
                _maxClip = _maxGrenadeLauncherClip;
                _gun = 4;
                _sound = 8;
            }

            if (_ammoCarry < _maxClip)
            {
                _ammoExcess -= _ammoCarry;
                _clip += _ammoCarry;
            }
            else
            {
                _ammoExcess -= _maxClip;
                _clip += _maxClip;
            }

            if (gunType == 1)
            {
                _pistolAmmoExcess = _ammoExcess;
                _pistolClip = _clip;
            }
            else if (gunType == 2)
            {
                _automaticRifleAmmoExcess = _ammoExcess;
                _automaticRifleClip = _clip;
            }
            else if (gunType == 3)
            {
                _shotgunAmmoExcess = _ammoExcess;
                _shotgunClip = _clip;
            }
            else if (gunType == 4)
            {
                _grenadeLauncherAmmoExcess = _ammoExcess;
                _grenadeLauncherClip = _clip;
            }

            _ammoExcess = Mathf.Max(0, _ammoExcess);

            GameManager.Instance.SetExcess(_gun, _ammoExcess);
            GameManager.Instance.SetCurrentExcess(_gun);

            GameManager.Instance.SetClip(_gun, _clip);
            GameManager.Instance.SetCurrentClip(_gun);

            Sound.Instance.PlayerGun(_sound);

            _reloadState = false;
        }
    }

    public void SwitchGuns(int arm)
    {
        int _gun = 0;
        int _sound = 0;

        if (arm == 1)
        {
            _equippedGun = _primaryGun;

            if (_equippedGun == _automaticRifleSprite)
            {
                _gun = 2;
                _sound = 4;
            }
            else if (_equippedGun == _shotgunSprite)
            {
                _gun = 3;
                _sound = 6;
            }
            else if (_equippedGun == _grenadeLauncherSprite)
            {
                _gun = 4;
                _sound = 8;
            }
        }
        else if (arm == 2)
        {
            if (_equippedGun == _primaryGun)
            {
                _equippedGun = _secondaryGun;

                _gun = 1;
                _sound = 2;
            }
        }

        if ((_primaryGun != null && _secondaryGun != null) && _currentGun != _gun)
        {
            UIManager.Instance.ActivateGun(_gun, _pistolSprite, _automaticRifleSprite, _shotgunSprite, _grenadeLauncherSprite);
            GameManager.Instance.SetCurrentClip(_gun);
            GameManager.Instance.SetCurrentExcess(_gun);

            Sound.Instance.PlayerGun(_sound);
        }

        _currentGun = _gun;
    }

    public void AddHealthKit()
    {
        if (_healthKitsCarry < _maxHealthKitCarry)
        {
            _healthKitsCarry++;
            GameManager.Instance.AddMedKit();
        }
    }

    public void UseHealthKit()
    {
        PlayerHealth playerHealth = this.gameObject.GetComponent<PlayerHealth>();

        if (_healthKitsCarry > 0)
        {
            Sound.Instance.Heal();

            _healthKitsCarry--;
            GameManager.Instance.UseMedKit();

            playerHealth.AddHealth(30);

            Debug.Log($"{_healthKitsCarry} left.") ;
        }
        else
        {
            Sound.Instance.DenyHeal();

            Debug.Log("Empty");
        }
    }
}