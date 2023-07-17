using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public AudioSource audioSource;

    [SerializeField] private Gun _equippedGun;

    public Gun _pistolSprite;
    public Gun _automaticRifleSprite;
    public Gun _shotgunSprite;

    int _mode = 0;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        UIManager.Instance.ActivateGun(_mode, _pistolSprite, _automaticRifleSprite, _shotgunSprite);
    }

    public override void Fire()
    {
        _equippedGun.EnemyShoot();
    }
}
