using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    // Reference: https://stackoverflow.com/questions/65622436/detecting-unity-button-being-held-down-and-not-just-pressed

    public bool _isHeldDown = false;

    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 2)
        {
            if (Inventory.Instance.CheckClip(2) == true)
            {
                Sound.Instance.PlayerGun(3);

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(2, 1);

                Debug.Log("Auto Fire");
            }
            else if (Inventory.Instance.CheckAmmo(2) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.PlayerGun(4);
            }
            else if (Inventory.Instance.CheckClip(2) == false)
            {
                Reload();
            }
        }
    }

    public void Update()
    {
        if (_isHeldDown == true && _fireOn == true)
        {
            Shoot();

            _fireOn = false;
            StartCoroutine(CO_StopFire());
        }
    }

    public override void Reload()
    {
        Inventory.Instance.ReloadGun(2);
    }

    public void OnPress()
    {
        _isHeldDown = true;
    }

    public void OnRelease()
    {
        _isHeldDown = false;
    }

    public override void EnemyShoot()
    {
        if (_enemyFireOn == true)
        {
            Sound.Instance.EnemyGun(this.transform, 3);

            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

            _enemyCount++;

            if (_enemyCount >= 30)
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
        yield return new WaitForSecondsRealtime(0.35f);
        _fireOn = true;
    }


    IEnumerator CO_EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        _enemyFireOn = true;
    }

    IEnumerator CO_EnemyReload()
    {
        _enemyFireOn = false;

        Sound.Instance.EnemyGun(this.transform, 4);
        yield return new WaitForSecondsRealtime(4.6f);
        _enemyFireOn = true;

        _enemyCount = 0;
    }
}