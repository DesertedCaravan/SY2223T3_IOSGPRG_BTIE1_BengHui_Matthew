using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 3)
        {
            if (Inventory.Instance.CheckClip(3) == true && _fireOn == true)
            {
                Sound.Instance.PlayerGun(5);

                for (int i = 0; i < 8; i++)
                {
                    GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
                }

                Inventory.Instance.UseAmmo(3, 1);

                Debug.Log("Cone Fire");

                StartCoroutine(CO_StopFire());
                _fireOn = false;
            }
            else if (Inventory.Instance.CheckAmmo(3) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.PlayerGun(6);
            }
            else if (Inventory.Instance.CheckClip(3) == false)
            {
                Reload();
            }
        }
    }

    public override void Reload()
    {
        Inventory.Instance.ReloadGun(3);
    }

    public override void EnemyShoot()
    {
        if (_enemyFireOn == true)
        {
            Sound.Instance.EnemyGun(audioSource, 5);

            for (int i = 0; i < 8; i++)
            {
                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
            }

            _enemyCount++;

            _enemyFireOn = false;
            StartCoroutine(CO_EnemyStopFire());
        }

        if (_enemyFireOn == true && _enemyCount >= 2)
        {
            _enemyFireOn = false;
            StartCoroutine(CO_EnemyReload());
        }
    }

    IEnumerator CO_StopFire()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        _fireOn = true;
    }

    IEnumerator CO_EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        _enemyFireOn = true;
    }

    IEnumerator CO_EnemyReload()
    {
        Sound.Instance.EnemyGun(audioSource, 6);
        yield return new WaitForSecondsRealtime(5.4f);
        _enemyFireOn = true;

        _enemyCount = 0;
    }
}