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
    // Reference: https://forum.unity.com/threads/rigidbody2d-addforce.493958/#:~:text=Code%20%28csharp%29%3A%20rigidbody2D.AddForce%20%28new%20Vector2%20%280f%2C,jumpForce%29%2C%20ForceMode2D.Impulse%29%3B%20to%20make%20character%20jump.

    [SerializeField] public GunType _gunType;

    [SerializeField] private int _clipMaxSize;

    [SerializeField] public float _fireSpeed;
    [SerializeField] public float _fireRate;
    [SerializeField] private float _spreadAmount;

    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject turret;

    private int _currentClip;

    public void Start()
    {
        _fireSpeed = 20.0f;
        _fireRate = 0.0f;
    }

    public virtual void Shoot()
    {
        // _gunType == GunType.Pistol

        Debug.Log("Base gun shooting");

        // b.GetComponent<Bullet>().Initialize(_fireSpeed, _fireRate);

        // b.GetComponent<Rigidbody2D>().AddForce(Mathf.Atan2(turret.transform.position.y, turret.transform.position.x) * _fireSpeed);
        // b.GetComponent<Rigidbody2D>().AddForce(new Vector2(turret.transform.position.x * _fireSpeed, turret.transform.position.y * _fireSpeed));
        // b.GetComponent<Rigidbody2D>().AddForce(turret.transform.forward * _fireSpeed);
    }

    public virtual void Reload()
    {
        Debug.Log("Base gun reloading");
    }
}