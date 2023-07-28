using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : PickupSpawner
{
    [Header("Barrel")]

    private Vector3 scaleChange;

    public int _health = 40;
    public bool _destroyed = false;

    public override void Start()
    {
        scaleChange = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public void TakeDamage()
    {
        _health -= 10;
        this.transform.localScale -= scaleChange;

        if (_health <= 0 && _destroyed == false)
        {
            SpawnPickups(Random.Range(3, 6), 2);
            _destroyed = true;

            Destroy(this.gameObject);
        }
    }
}
