using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Event
    public UnityEvent OnAmmoChange;
    public UnityEvent OnClipChange;
    public UnityEvent OnExcessChange;

    // [SerializeField] public Slider lifeSlider;

    // public int _score = 0;
    // public int _life = 0;

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
}
