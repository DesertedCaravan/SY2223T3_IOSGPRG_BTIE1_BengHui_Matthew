using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 1)
        {
            if (Inventory.Instance.CheckClip(1) == true && _fireOn == true)
            {
                Sound.Instance.PlayerGun(1);

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(1, 1);

                Debug.Log("Single Fire");

                StartCoroutine(CO_StopFire());
                _fireOn = false;
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
            StartCoroutine(CO_EnemyStopFire());
        }

        if (_enemyFireOn == true && _enemyCount >= 15)
        {
            _enemyFireOn = false;
            StartCoroutine(CO_EnemyReload());
        }
    }

    IEnumerator CO_StopFire()
    {
        yield return new WaitForSecondsRealtime(2.16f);
        _fireOn = true;
    }

    IEnumerator CO_EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(2.16f);
        _enemyFireOn = true;
    }

    IEnumerator CO_EnemyReload()
    {
        Sound.Instance.EnemyGun(audioSource, 2);
        yield return new WaitForSecondsRealtime(4.0f);
        _enemyFireOn = true;

        _enemyCount = 0;
    }
}