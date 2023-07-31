using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 4)
        {
            if (Inventory.Instance.CheckClip(4) == true && _fireOn == true)
            {
                Sound.Instance.PlayerGun(7);

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(4, 1);

                Debug.Log("Grenade Fire");

                StartCoroutine(CO_StopFire());
                _fireOn = false;
            }
            else if (Inventory.Instance.CheckAmmo(4) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.PlayerGun(8);
            }
            else if (Inventory.Instance.CheckClip(4) == false)
            {
                Reload();
            }
        }
    }

    public override void Reload()
    {
        Inventory.Instance.ReloadGun(4);
    }

    public override void EnemyShoot()
    {
        if (_enemyFireOn == true)
        {
            Sound.Instance.EnemyGun(this.transform, 7);

            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

            _enemyCount++;

            if (_enemyCount >= 1)
            {
                _enemyReloadOn = true;
            }
            else
            {
                _enemyFireOn = false;
                StartCoroutine(CO_EnemyStopFire());
            }
        }

        if (_enemyReloadOn == true)
        {
            _enemyReloadOn = false;
            StartCoroutine(CO_EnemyReload());
        }
    }

    IEnumerator CO_StopFire()
    {
        yield return new WaitForSecondsRealtime(5f);
        _fireOn = true;
    }

    IEnumerator CO_EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(5f);
        _enemyFireOn = true;
    }

    IEnumerator CO_EnemyReload()
    {
        _enemyFireOn = false;
        Sound.Instance.EnemyGun(this.transform, 8);
        yield return new WaitForSecondsRealtime(4.6f);

        _enemyFireOn = true;

        _enemyCount = 0;
    }
}