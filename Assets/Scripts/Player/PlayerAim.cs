using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Unit unit;

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
            //  move forward on local x when moving joystick up or down
            float xRotation = joystickMovement.joystickVec.x;
            float yRotation = joystickMovement.joystickVec.y;

            if (joystickMovement.joystickVec.y > 0)
            {
                rb.SetRotation(xRotation * -90);
            }
            else if (joystickMovement.joystickVec.y < 0)
            {
                rb.SetRotation((xRotation * 90) + 180);
            }
        }
    }

    public void CallDeath()
    {
        _deathState = true;
    }
}
