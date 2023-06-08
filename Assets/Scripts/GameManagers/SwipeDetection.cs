using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeDetection : Singleton<SwipeDetection>
{
    [SerializeField] private TextMeshProUGUI debugText;

    // Coordinates on the Touch Screen
    private Vector2 _initialTouchPosition;
    private Vector2 _endTouchPosition;

    private bool _gameStart = true;

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

            // (0, 0) is located at the bottomleft corner of the screen
            if (_endTouchPosition.x <= 500.0f && _endTouchPosition.y <= 350.0f)
            {
                GameManager.Instance.SuperMode();
            }
            else
            {
                CheckSwipe();
            }

            if ((_endTouchPosition.x >= 260.0f && _endTouchPosition.x <= 850.0f) && _gameStart == true)
            {
                if (_endTouchPosition.y >= 1300.0f && _endTouchPosition.y <= 1500.0f)
                {
                    UIManager.Instance.DefaultSelected();
                }
                else if (_endTouchPosition.y >= 1000.0f && _endTouchPosition.y <= 1200.0f)
                {
                    UIManager.Instance.TankSelected();
                }
                else if (_endTouchPosition.y >= 700.0f && _endTouchPosition.y <= 900.0f)
                {
                    UIManager.Instance.SpeedSelected();
                }

                _gameStart = false;
            }

            if ((_endTouchPosition.x >= 260.0f && _endTouchPosition.x <= 850.0f) && (_endTouchPosition.y >= 400.0f && _endTouchPosition.y <= 600.0f))
            {
                GameManager.Instance.RestartGame();
            }
        }
    }

    private void CheckSwipe()
    {
        int _getDirection = 5;
        int _directionCheck = ArrowManager.Instance.GetArrowDirection();
        bool _switchCheck = ArrowManager.Instance.GetArrowSwitch();

        float xAbsolute = Mathf.Abs(_initialTouchPosition.x - _endTouchPosition.x);
        float yAbsolute = Mathf.Abs(_initialTouchPosition.y - _endTouchPosition.y);

        if (_initialTouchPosition.x < _endTouchPosition.x && xAbsolute > yAbsolute)
        {
            debugText.text = "Swiped Right.";
            _getDirection = 3;
        }
        else if (_initialTouchPosition.x > _endTouchPosition.x && xAbsolute > yAbsolute)
        {
            debugText.text = "Swiped Left.";
            _getDirection = 1;
        }
        else if (_initialTouchPosition.y < _endTouchPosition.y && yAbsolute > xAbsolute)
        {
            debugText.text = "Swiped Up.";
            _getDirection = 2;
        }
        else if (_initialTouchPosition.y > _endTouchPosition.y && yAbsolute > xAbsolute)
        {
            debugText.text = "Swiped Down.";
            _getDirection = 0;
        }

        if (_switchCheck == true && (_getDirection == 0 || _getDirection == 1))
        {
            _getDirection += 2;
        }
        else if (_switchCheck == true && (_getDirection == 2 || _getDirection == 3))
        {
            _getDirection -= 2;
        }

        if (_getDirection == _directionCheck)
        {
            Player.Instance.CorrectSwipe();
        }
        else
        {
            Player.Instance.WrongSwipe();
        }
    }

    public void StartMenu()
    {
        _gameStart = true;
    }
}