using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : Singleton<Sound>
{
    [Header("Ammo Sounds")]
    public AudioSource playerSource;
    public AudioClip pickupItem;

    public AudioClip pistolReload;
    public AudioClip pistolShoot;

    public AudioClip automaticRifleReload;
    public AudioClip automaticRifleShoot;

    public AudioClip shotgunReload;
    public AudioClip shotgunShoot;

    public AudioClip fullAmmo;

    [Header("Target Sounds")]
    public AudioClip hurtPlayer;
    public AudioClip hurtEnemy;
    public AudioClip deadPlayer;
    public AudioClip deadEnemy;

    public AudioClip heal;
    public AudioClip denyHeal;

    public AudioClip killAnnouncer1;
    public AudioClip killAnnouncer2;
    public AudioClip playerDeathAnnouncer;

    [Header("Obstacle Sounds")]
    public AudioClip barrelBreak;

    public AudioClip woodenBulletHit;
    public AudioClip stoneBulletHit;
    public AudioClip metalBulletHit;
    public AudioClip plasticBulletHit;

    private void Start()
    {
        playerSource = GetComponent<AudioSource>();
    }

    public void PickupItem()
    {
        playerSource.PlayOneShot(pickupItem);
    }

    public void PlayerGun(int type)
    {
        if (type == 1)
        {
            playerSource.PlayOneShot(pistolShoot);
        }
        else if (type == 2)
        {
            playerSource.PlayOneShot(pistolReload);
        }
        else if (type == 3)
        {
            playerSource.PlayOneShot(automaticRifleShoot);
        }
        else if (type == 4)
        {
            playerSource.PlayOneShot(automaticRifleReload);
        }
        else if (type == 5)
        {
            playerSource.PlayOneShot(shotgunShoot);
        }
        else if (type == 6)
        {
            playerSource.PlayOneShot(shotgunReload);
        }
    }

    public void FullAmmo()
    {
        playerSource.PlayOneShot(fullAmmo);
    }

    public void EnemyGun(AudioSource enemySource, int type)
    {
        if (type == 1)
        {
            enemySource.PlayOneShot(pistolShoot);
        }
        else if (type == 2)
        {
            enemySource.PlayOneShot(pistolReload);
        }
        else if (type == 3)
        {
            enemySource.PlayOneShot(automaticRifleShoot);
        }
        else if (type == 4)
        {
            enemySource.PlayOneShot(automaticRifleReload);
        }
        else if (type == 5)
        {
            enemySource.PlayOneShot(shotgunShoot);
        }
        else if (type == 6)
        {
            enemySource.PlayOneShot(shotgunReload);
        }
    }

    public void HurtPlayer()
    {
        playerSource.PlayOneShot(hurtPlayer);
    }

    public void HurtEnemy()
    {
        playerSource.PlayOneShot(hurtEnemy);
    }

    public void DeadPlayer()
    {
        playerSource.PlayOneShot(deadPlayer);
    }

    public void DeadEnemy()
    {
        playerSource.PlayOneShot(deadEnemy);
    }

    public void Heal()
    {
        playerSource.PlayOneShot(heal);
    }

    public void DenyHeal()
    {
        playerSource.PlayOneShot(denyHeal);
    }

    public void BarrelBreak()
    {
        playerSource.PlayOneShot(barrelBreak);
    }

    public void ObstacleBulletHit(AudioSource environmentSource, int type)
    {
        if (type == 1)
        {
            environmentSource.PlayOneShot(woodenBulletHit);
        }
        else if (type == 2)
        {
            environmentSource.PlayOneShot(stoneBulletHit);
        }
        else if (type == 3)
        {
            environmentSource.PlayOneShot(metalBulletHit);
        }
        else if (type == 4)
        {
            environmentSource.PlayOneShot(plasticBulletHit);
        }
    }

    public void KillAnnouncer(int type)
    {
        if (type == 1)
        {
            playerSource.PlayOneShot(killAnnouncer1);
        }
        else if (type == 2)
        {
            playerSource.PlayOneShot(killAnnouncer2);
        }
    }

    public void PlayerDeathAnnouncer()
    {
        playerSource.PlayOneShot(playerDeathAnnouncer);
    }
}