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

    public AudioClip playerVictory;

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

    public void EnemyGun(Transform enemyPosition, int type)
    {
        if (type == 1)
        {
            AudioSource.PlayClipAtPoint(pistolShoot, enemyPosition.position, 30f);
        }
        else if (type == 2)
        {
            AudioSource.PlayClipAtPoint(pistolReload, enemyPosition.position, 30f);
        }
        else if (type == 3)
        {
            AudioSource.PlayClipAtPoint(automaticRifleShoot, enemyPosition.position, 30f);
        }
        else if (type == 4)
        {
            AudioSource.PlayClipAtPoint(automaticRifleReload, enemyPosition.position, 30f);
        }
        else if (type == 5)
        {
            AudioSource.PlayClipAtPoint(shotgunShoot, enemyPosition.position, 30f);
        }
        else if (type == 6)
        {
            AudioSource.PlayClipAtPoint(shotgunReload, enemyPosition.position, 30f);
        }
    }

    public void HurtPlayer()
    {
        playerSource.PlayOneShot(hurtPlayer);
    }

    public void HurtEnemy(Transform enemyPosition)
    {
        AudioSource.PlayClipAtPoint(hurtEnemy, enemyPosition.position, 30f);
    }

    public void DeadPlayer()
    {
        playerSource.PlayOneShot(deadPlayer);
    }

    public void DeadEnemy(Transform enemyPosition)
    {
        AudioSource.PlayClipAtPoint(deadEnemy, enemyPosition.position, 30f);
    }

    public void Heal()
    {
        playerSource.PlayOneShot(heal);
    }

    public void DenyHeal()
    {
        playerSource.PlayOneShot(denyHeal);
    }

    public void BarrelBreak(Transform barrelPosition)
    {
        AudioSource.PlayClipAtPoint(barrelBreak, barrelPosition.position, 15f);
    }

    public void ObstacleBulletHit(Transform environmentPosition, int type)
    {
        if (type == 1)
        {
            AudioSource.PlayClipAtPoint(woodenBulletHit, environmentPosition.position, 15f);
        }
        else if (type == 2)
        {
            AudioSource.PlayClipAtPoint(stoneBulletHit, environmentPosition.position, 15f);
        }
        else if (type == 3)
        {
            AudioSource.PlayClipAtPoint(metalBulletHit, environmentPosition.position, 15f);
        }
        else if (type == 4)
        {
            AudioSource.PlayClipAtPoint(plasticBulletHit, environmentPosition.position, 15f);
        }
    }

    public void KillAnnouncer(Transform killPosition, int type)
    {
        if (type == 1)
        {
            AudioSource.PlayClipAtPoint(killAnnouncer1, killPosition.position, 60f);
        }
        else if (type == 2)
        {
            AudioSource.PlayClipAtPoint(killAnnouncer2, killPosition.position, 60f);
        }
    }

    public void PlayerDeathAnnouncer()
    {
        playerSource.PlayOneShot(playerDeathAnnouncer);
    }

    public void PlayerVictory()
    {
        playerSource.PlayOneShot(playerVictory);
    }
}