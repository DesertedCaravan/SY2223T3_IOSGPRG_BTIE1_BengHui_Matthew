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
                Sound.Instance.PistolShoot();

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(1, 1);

                Debug.Log("Single Fire");
            }
            else if (Inventory.Instance.CheckAmmo(1) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.PistolReload();
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
}