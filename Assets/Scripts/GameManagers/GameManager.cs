using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Event
    public UnityEvent OnHighScoreChange;
    public UnityEvent OnSurvivorsChange;

    public UnityEvent OnMedKitChange;
    public UnityEvent OnAmmoChange;
    public UnityEvent OnClipChange;
    public UnityEvent OnExcessChange;
    public UnityEvent OnWinStateChange;

    // [SerializeField] public Slider lifeSlider;

    public int _highScore = 0;
    public int _survivors = 20;

    public int _life = 0;
    public int _medKit = 0;

    public int _currentClip = 0;
    public int _currentExcess = 0;

    public int _pistolAmmo = 0;
    public int _automaticRifleAmmo = 0;
    public int _shotgunAmmo = 0;

    public int _pistolExcess = 0;
    public int _automaticRifleExcess = 0;
    public int _shotgunExcess = 0;

    public int _pistolClip = 0;
    public int _automaticRifleClip = 0;
    public int _shotgunClip = 0;

    public int _enemyTally = 0;
    public string _winState;

    public void IncreaseHighScore()
    {
        _highScore += 10;
        OnHighScoreChange?.Invoke();
    }

    public void DecreaseSurvivors()
    {
        _survivors--;
        OnSurvivorsChange?.Invoke();

        if (_survivors <= 0)
        {
            Winner();
        }
    }

    public void AddMedKit()
    {
        _medKit++;
        OnMedKitChange?.Invoke();
    }

    public void UseMedKit()
    {
        _medKit--;
        OnMedKitChange?.Invoke();
    }

    public void SetCurrentClip(int gunType)
    {
        if (gunType == 1)
        {
            _currentClip = _pistolClip;
        }
        else if (gunType == 2)
        {
            _currentClip = _automaticRifleClip;
        }
        else if (gunType == 3)
        {
            _currentClip = _shotgunClip;
        }

        OnClipChange?.Invoke();
    }

    public void SetCurrentExcess(int gunType)
    {
        if (gunType == 1)
        {
            _currentExcess = _pistolExcess;
        }
        else if (gunType == 2)
        {
            _currentExcess = _automaticRifleExcess;
        }
        else if (gunType == 3)
        {
            _currentExcess = _shotgunExcess;
        }

        OnExcessChange?.Invoke();
    }

    public void SetAmmo(int gunType, int amount)
    {
        if (gunType == 1)
        {
            _pistolAmmo = amount;
        }
        else if (gunType == 2)
        {
            _automaticRifleAmmo = amount;
        }
        else if (gunType == 3)
        {
            _shotgunAmmo = amount;
        }
        
        OnAmmoChange?.Invoke();
    }

    public void SetClip(int gunType, int amount)
    {
        if (gunType == 1)
        {
            _pistolClip = amount;
        }
        else if (gunType == 2)
        {
            _automaticRifleClip = amount;
        }
        else if (gunType == 3)
        {
            _shotgunClip = amount;
        }
    }

    public void SetExcess(int gunType, int amount)
    {
        if (gunType == 1)
        {
            _pistolExcess = amount;
        }
        else if (gunType == 2)
        {
            _automaticRifleExcess = amount;
        }
        else if (gunType == 3)
        {
            _shotgunExcess = amount;
        }
    }

    public void Reloading()
    {
        _winState = "Reloading...";
        OnWinStateChange?.Invoke();
    }

    public void ReloadingComplete()
    {
        _winState = " ";
        OnWinStateChange?.Invoke();
    }

    public void Winner()
    {
        Sound.Instance.PlayerVictory();

        _winState = "YOU WIN";
        OnWinStateChange?.Invoke();
    }

    public void GameOver()
    {
        _winState = "GAME OVER";
        OnWinStateChange?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
