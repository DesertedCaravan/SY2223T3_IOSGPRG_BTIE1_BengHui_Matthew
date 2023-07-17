using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 1)
        {
            if (Inventory.Instance.CheckClip(1) == true)
            {
                Sound.Instance.PlayerGun(1);

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(1, 1);

                Debug.Log("Single Fire");
            }
            else if (Inventory.Instance.CheckAmmo(1) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.PlayerGun(2);
            }
            else if (Inventory.Instance.CheckClip(1) == false)
            {
                Reload();
            }
        }
    }

    public override void Reload()
    {
        Inventory.Instance.ReloadGun(1);
    }

    public override void EnemyShoot()
    {
        if (_enemyFireOn == true)
        {
            Sound.Instance.EnemyGun(audioSource, 1);

            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

            _enemyCount++;

            _enemyFireOn = false;
            StartCoroutine(EnemyStopFire());
        }

        if (_enemyFireOn == true && _enemyCount >= 15)
        {
            _enemyFireOn = false;
            StartCoroutine(EnemyReload());
        }
    }

    IEnumerator EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _enemyFireOn = true;
    }

    IEnumerator EnemyReload()
    {
        Sound.Instance.EnemyGun(audioSource, 2);
        yield return new WaitForSecondsRealtime(3.0f);
        _enemyFireOn = true;

        _enemyCount = 0;
    }
}