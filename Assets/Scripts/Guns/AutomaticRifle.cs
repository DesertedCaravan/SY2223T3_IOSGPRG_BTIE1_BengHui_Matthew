using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    // Reference: https://stackoverflow.com/questions/65622436/detecting-unity-button-being-held-down-and-not-just-pressed

    public bool _isHeldDown = false;
    public bool _fireOn = true;

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
            StartCoroutine(StopFire());
        }
    }

    public override void Reload()
    {
        Inventory.Instance.ReloadGun(2);
    }

    public void onPress()
    {
        _isHeldDown = true;
    }

    public void onRelease()
    {
        _isHeldDown = false;
    }

    IEnumerator StopFire()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _fireOn = true;
    }

    public override void EnemyShoot()
    {
        if (_enemyFireOn == true)
        {
            Sound.Instance.EnemyGun(audioSource, 3);

            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

            _enemyCount++;

            _enemyFireOn = false;
            StartCoroutine(EnemyStopFire());
        }

        if (_enemyFireOn == true && _enemyCount >= 30)
        {
            _enemyFireOn = false;
            StartCoroutine(EnemyReload());
        }
    }

    IEnumerator EnemyStopFire()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        _enemyFireOn = true;
    }

    IEnumerator EnemyReload()
    {
        Sound.Instance.EnemyGun(audioSource, 4);
        yield return new WaitForSecondsRealtime(3.0f);
        _enemyFireOn = true;

        _enemyCount = 0;
    }
}