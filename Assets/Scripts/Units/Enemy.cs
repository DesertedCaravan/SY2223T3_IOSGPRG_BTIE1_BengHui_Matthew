using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private Gun _equippedGun;

    public Gun _pistolSprite;
    public Gun _automaticRifleSprite;
    public Gun _shotgunSprite;
    public Gun _grenadeLauncherSprite;

    int _mode = 0;

    public void Start()
    {
        _mode = Random.Range(1, 4);

        if (_mode == 1)
        {
            _equippedGun = _pistolSprite;
        }
        else if (_mode == 2)
        {
            _equippedGun = _automaticRifleSprite;
        }
        else if (_mode == 3)
        {
            _equippedGun = _shotgunSprite;
        }
        else if (_mode == 4)
        {
            _equippedGun = _grenadeLauncherSprite;
        }

        UIManager.Instance.ActivateGun(_mode, _pistolSprite, _automaticRifleSprite, _shotgunSprite, _grenadeLauncherSprite);
    }

    public override void Fire()
    {
        _equippedGun.EnemyShoot();
    }
}
