using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot()
    {
        if (Inventory.Instance.CheckGun() == 3)
        {
            if (Inventory.Instance.CheckClip(3) == true)
            {
                Sound.Instance.ShotgunShoot();

                GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);

                Inventory.Instance.UseAmmo(3, 1);

                Debug.Log("Cone Fire");
            }
            else if (Inventory.Instance.CheckAmmo(3) == false)
            {
                Debug.Log("Out of Ammo!");

                Sound.Instance.ShotgunReload();
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
}