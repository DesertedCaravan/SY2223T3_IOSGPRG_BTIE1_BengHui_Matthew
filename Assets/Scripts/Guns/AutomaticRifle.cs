using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 2)
        {
            if (Inventory.Instance.CheckClip(2) == true)
            {
                Sound.Instance.AutomaticRifleShoot();

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(2, 1);

                Debug.Log("Auto Fire");
            }
            else if (Inventory.Instance.CheckAmmo(2) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.AutomaticRifleReload();
            }
            else if (Inventory.Instance.CheckClip(2) == false)
            {
                Reload();
            }
        }
    }
    public override void Reload()
    {
        Inventory.Instance.ReloadGun(2);
    }
}