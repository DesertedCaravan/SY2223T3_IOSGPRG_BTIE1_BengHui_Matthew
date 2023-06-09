using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    // Reference: https://www.youtube.com/watch?v=TPzOgMfoCGY
    public GameObject joystick;
    public GameObject joystickBG;

    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;

    private float joystickRadius;

    void Start()
    {
        // Starting Area of Joystick
        joystickOriginalPos = joystickBG.transform.position;

        // Area that Joystick can move around in
        // * means bigger area
        // / means smaller area
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y * 1.5f;
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }

}