using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    AutomaticRifle,
    Shotgun
}

public class Gun : MonoBehaviour
{
    [SerializeField] private GunType _gunType;

    [SerializeField] private int _clipMaxSize;

    [SerializeField] private float _fireRate;
    [SerializeField] private float _spreadAmount;

    private int _currentClip;

    public virtual void Shoot()
    {
        Debug.Log("Base gun shooting");
    }

    public void Reload()
    {
        Debug.Log("Base gun reloading");
    }
}