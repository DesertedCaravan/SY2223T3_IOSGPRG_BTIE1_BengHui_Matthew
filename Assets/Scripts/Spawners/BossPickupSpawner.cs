using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPickupSpawner : PickupSpawner
{
    public override void Start()
    {
    }

    public void SpawnBossDrops()
    {
        SpawnBossPickups(Random.Range(2, 5));
    }
}
