using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Unit
{
    public override void Initialize(string name, int maxHealth, float speed)
    {
        Debug.Log("WARNING BOSS IS SPAWNING");

        // base refers to the base class, which carries over to this class
        base.Initialize(name, maxHealth, speed);

        Debug.Log("BOSS ENEMY SPAWNED");
    }
}