using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : Singleton<ArrowManager>
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites; // is static and won't add/remove in real time, while "List<Sprite> sprites" is is dynamic

    private bool _gameOn = false;
    private bool _coroutine = false;

    int _colorSwitch;
    int _previousArrow = 0;
    int _currentArrow;

    bool _arrowSwitch;

    void Update()
    {
        if (_gameOn == true)
        {
            StartCoroutine(CO_Timer());

            _gameOn = false;
        }
    }

    public void GameOn()
    {
        _gameOn = true;
    }

    public void CorouteOn()
    {
        _coroutine = true;
    }

    public void CoroutePause()
    {
        _coroutine = false;
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
        while (_coroutine)
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
        }
    }
}