using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Unit
{
    [SerializeField] private Gun _equippedGun;

    public Gun _grenadeLauncherSprite;

    int _mode = 4;

    public void Start()
    {
        _equippedGun = _grenadeLauncherSprite;

        UIManager.Instance.ActivateGun(_mode, _grenadeLauncherSprite);
    }

    public override void Fire()
    {
        _equippedGun.EnemyShoot();
    }
}