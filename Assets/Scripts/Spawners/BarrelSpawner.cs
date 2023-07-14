using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : PickupSpawner
{
    public int _health = 40;
    public bool _destroyed = false;

    public override void Start()
    {
        
    }

    public void TakeDamage()
    {
        _health -= 10;

        if (_health <= 0 && _destroyed == false)
        {
            Initialize();
            _destroyed = true;

            Destroy(this.gameObject);
        }
    }
}
