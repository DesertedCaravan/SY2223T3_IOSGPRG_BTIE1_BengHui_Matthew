using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Unit unit;

    // Reference: https://www.youtube.com/watch?v=TPzOgMfoCGY
    public JoystickMovement joystickMovement;
    private Rigidbody2D rb;

    public bool _deathState = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_deathState == false)
        {
            if (joystickMovement.joystickVec.y != 0)
            {
                rb.velocity = new Vector2(joystickMovement.joystickVec.x * unit._speed, joystickMovement.joystickVec.y * unit._speed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void CallDeath()
    {
        _deathState = true;
    }
}