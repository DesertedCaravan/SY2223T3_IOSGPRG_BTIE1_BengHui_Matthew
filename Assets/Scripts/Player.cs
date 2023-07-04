using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Reference: https://www.youtube.com/watch?v=TPzOgMfoCGY
    public MovementJoystick movementJoystick;
    private Rigidbody2D rb;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip ammoCollect;
    public AudioClip hurtEnemy;

    private void Start()
    {
        Initialize("Player", 100, 5);

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * _speed, movementJoystick.joystickVec.y * _speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only possible if the Game Object that collides with Player has the Health class attached to it.
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        Health health = collision.gameObject.GetComponent<Health>();

        if (pickup != null)
        {
            audioSource.PlayOneShot(ammoCollect);
        }

        if (health != null)
        {
            health.TakeDamage(5);
            audioSource.PlayOneShot(hurtEnemy);
        }

    }
}