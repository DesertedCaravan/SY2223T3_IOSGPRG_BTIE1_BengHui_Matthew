using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private void Start()
    {
        Initialize("Player", 100, 5);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only possible if the Game Object that collides with Player has the Health class attached to it.
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            Sound.Instance.HurtEnemy();
            health.TakeDamage(5);
        }
    }
}