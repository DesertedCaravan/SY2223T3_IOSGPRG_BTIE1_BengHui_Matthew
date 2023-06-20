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

    private bool _dashState = false;

    // Update is called once per frame
    void Update()
    {
        // There is one or more Touches being registered and the first one is Began
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // get first touch
            Touch touch = Input.GetTouch(0);
            _initialTouchPosition = touch.position;
        }
        // There is one or more Touches being registered and the first one is Ended
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // get first touch
            Touch touch = Input.GetTouch(0);
            _endTouchPosition = touch.position;

            // (0, 0) is located at the bottomleft corner of the screen

            CheckSwipe();

            bool _checkStart = UIManager.Instance.GetGameState();
            bool _gaugeState = GameManager.Instance.GetGaugeState();

            if ((_checkStart == true) && (_dashState == false && _gaugeState == false))
            {
                StartCoroutine(CO_Dash());
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

    private IEnumerator CO_Dash()
    {
        _dashState = true;
        GameManager.Instance.AddScore(1);

        yield return new WaitForSeconds(1.0f);

        _dashState = false;
    }
}