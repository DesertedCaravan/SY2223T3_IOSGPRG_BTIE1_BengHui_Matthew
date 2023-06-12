using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : Singleton<ArrowManager>
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites; // is static and won't add/remove in real time, while "List<Sprite> sprites" is is dynamic

    private bool _active = false;

    int _colorSwitch;
    int _previousArrow = 0;
    int _currentArrow;

    bool _arrowSwitch;

    public void GameStart()
    {
        StartCoroutine(CO_Timer());
    }

    public void GameOn()
    {
        _active = true;
    }

    public void GamePause()
    {
        _active = false;
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
        while (_active)
        {
            yield return new WaitForSeconds(1.0f);

            _colorSwitch = Random.Range(0, 4);

            // 25% chance of being red
            if (_colorSwitch == 0)
            {
                image.color = new Color(255, 0, 0);

                _arrowSwitch = true;
            }
            // 75% chance of being green
            else
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