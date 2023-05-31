using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : Singleton<ArrowManager>
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites; // is static, won't add/remove in real time
    // List<Sprite> sprites; is dynamic

    [SerializeField] private bool _inRange;

    int _colorSwitch;
    int _previousArrow = 0;
    int _currentArrow;

    bool _arrowSwitch;

    // float timer;

    private void Start()
    {
       StartCoroutine(CO_Timer());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlayerInRange()
    {
        _inRange = true;
    }

    public void PlayerOutRange()
    {
        _inRange = false;
    }

    public int GetArrowDirection()
    {
        return _currentArrow;
    }

    public bool GetArrowSwitch()
    {
        return _arrowSwitch;
    }

    private IEnumerator CO_Timer()
    {
        // Debug.Log("First Call");

        while (!_inRange)
        {
            yield return new WaitForSeconds(1.0f);

            _colorSwitch = Random.Range(0, 4);

            if (_colorSwitch == 0) // 25% chance of being red
            {
                image.color = new Color(255, 0, 0);

                _arrowSwitch = true;
            }
            else // 75% chance of being green
            {
                image.color = new Color(0, 255, 0);

                _arrowSwitch = false;
            }

            do
            {
                _currentArrow = Random.Range(0, sprites.Length);
            }
            while (_currentArrow == _previousArrow);

            image.sprite = sprites[_currentArrow];

            _previousArrow = _currentArrow;
            
            // Debug.Log("Delayed Call");
        }

        // image.sprite = sprites[0];
    }
}

// Unused Code
/*
        timer += Time.deltaTime;

        if (timer > 1)
        {
            Debug.Log("Tick");
            timer = 0;
        }
 */