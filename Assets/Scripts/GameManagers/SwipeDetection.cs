using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    // Coordinates on the Touch Screen
    private Vector2 _initialTouchPosition;
    private Vector2 _endTouchPosition;

    // Update is called once per frame
    void Update()
    {
        // There is one or more Touches being registered and the first one is Began
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0); // get first touch
            _initialTouchPosition = touch.position;
        }
        // There is one or more Touches being registered and the first one is Ended
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0); // get first touch
            _endTouchPosition = touch.position;

            CheckSwipe();
        }
    }

    private void CheckSwipe()
    {
        int _directionCheck = ArrowManager.Instance.GetArrowDirection();
        bool _switchCheck = ArrowManager.Instance.GetArrowSwitch();

        float xAbsolute = Mathf.Abs(_initialTouchPosition.x - _endTouchPosition.x);
        float yAbsolute = Mathf.Abs(_initialTouchPosition.y - _endTouchPosition.y);

        if (_initialTouchPosition.x < _endTouchPosition.x && xAbsolute > yAbsolute)
        {
            debugText.text = "Swiped Right.";

            if ((_directionCheck == 3 && _switchCheck == false) || (_directionCheck == 1 && _switchCheck == true))
            {
                // Debug.Log("Success");
                Player.Instance.CorrectSwipe();
            }
            else
            {
                Player.Instance.WrongSwipe();
            }
        }
        else if (_initialTouchPosition.x > _endTouchPosition.x && xAbsolute > yAbsolute)
        {
            debugText.text = "Swiped Left.";

            if ((_directionCheck == 1 && _switchCheck == false) || (_directionCheck == 3 && _switchCheck == true))
            {
                // Debug.Log("Success");
                Player.Instance.CorrectSwipe();
            }
            else
            {
                Player.Instance.WrongSwipe();
            }
        }
        else if (_initialTouchPosition.y < _endTouchPosition.y && yAbsolute > xAbsolute)
        {
            debugText.text = "Swiped Up.";

            if ((_directionCheck == 2 && _switchCheck == false) || (_directionCheck == 0 && _switchCheck == true))
            {
                // Debug.Log("Success");
                Player.Instance.CorrectSwipe();
            }
            else
            {
                Player.Instance.WrongSwipe();
            }
        }
        else if (_initialTouchPosition.y > _endTouchPosition.y && yAbsolute > xAbsolute)
        {
            debugText.text = "Swiped Down.";

            if ((_directionCheck == 0 && _switchCheck == false) || (_directionCheck == 2 && _switchCheck == true))
            {
                // Debug.Log("Success");
                Player.Instance.CorrectSwipe();
            }
            else
            {
                Player.Instance.WrongSwipe();
            }
        }
    }
}

// Unused Code
/*
        float distance = Vector2.Distance(initialTouchPosition, endTouchPosition);

        if (distance > 0)

--------------------------------------------------------------------------------------

    private bool PointIsOnLeft(Vector2 point1, Vector2 point2, Vector2 point3)
    {
        return (point2.x - point1.x) * (point3.y - point1.y) - (point2.y - point1.y) * (point3.x - point1.x) > 0;
    }

--------------------------------------------------------------------------------------

            // Debug.Log("Swiped Right.");
            // Debug.Log("Swiped Left.");
            // Debug.Log("Swiped Up.");
            // Debug.Log("Swiped Down.");
*/