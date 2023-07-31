using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExplosionType
{
    PlayerExplosion,
    EnemyExplosion
}

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private ExplosionType _explosionType;

    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }

    public ExplosionType GetExplosionType()
    {
        return _explosionType;
    }
}
