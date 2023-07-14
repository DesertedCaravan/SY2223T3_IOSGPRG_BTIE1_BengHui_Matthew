using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : Singleton<Sound>
{
    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip pickupItem;
    public AudioClip hurtEnemy;

    public AudioClip pistolReload;
    public AudioClip pistolShoot;

    public AudioClip automaticRifleReload;
    public AudioClip automaticRifleShoot;

    public AudioClip shotgunReload;
    public AudioClip shotgunShoot;

    public AudioClip fullAmmo;
    public AudioClip barrelBreak;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickupItem()
    {
        audioSource.PlayOneShot(pickupItem);
    }

    public void HurtEnemy()
    {
        audioSource.PlayOneShot(hurtEnemy);
    }

    public void PistolReload()
    {
        audioSource.PlayOneShot(pistolReload);
    }

    public void PistolShoot()
    {
        audioSource.PlayOneShot(pistolShoot);
    }

    public void AutomaticRifleReload()
    {
        audioSource.PlayOneShot(automaticRifleReload);
    }

    public void AutomaticRifleShoot()
    {
        audioSource.PlayOneShot(automaticRifleShoot);
    }

    public void ShotgunReload()
    {
        audioSource.PlayOneShot(shotgunReload);
    }

    public void ShotgunShoot()
    {
        audioSource.PlayOneShot(shotgunShoot);
    }

    public void FullAmmo()
    {
        audioSource.PlayOneShot(fullAmmo);
    }

    public void BarrelBreak()
    {
        audioSource.PlayOneShot(barrelBreak);
    }
}