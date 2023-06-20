using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Event
    public UnityEvent OnScoreRestart;
    public UnityEvent OnScoreChange;
    public UnityEvent OnLifeChange;
    [SerializeField] public Slider dashSlider;
    [SerializeField] public Image dashGauge;
    [SerializeField] public Button dashButton;

    public AudioSource audioSource;
    public AudioClip gaugeFill;

    public int _score = 0;
    public int _life = 0;
    public bool _gaugeDeplete = false;

    private void Start()
    {
        SetStartingGauge(100);
    }

    private void Update()
    {
        if (_gaugeDeplete == true)
        {
            DepleteGauge(0.05f);
        }
    }

    public void SetStartingGauge(int amount)
    {
        audioSource = GetComponent<AudioSource>();

        dashSlider.maxValue = amount;
    }

    public void AddGauge(int amount)
    {
        dashSlider.value += amount;

        if (dashSlider.value >= 100)
        {
            dashSlider.value = 100;

            dashGauge.color = new Color(1.0f, 1.0f, 0.0f);
            audioSource.PlayOneShot(gaugeFill);

            dashButton.gameObject.SetActive(true);
        }
    }

    public void DepleteGauge(float amount)
    {
        dashSlider.value -= amount;

        if (dashSlider.value <= 0)
        {
            _gaugeDeplete = false;

            RestartGauge();

            SpawnManager.Instance.DeactivateSuperMode();
            EnvironmentSpawner.Instance.DeactivateSuperMode();
            UIManager.Instance.DeactivateSuperMode();
        }
    }

    public void RestartGauge()
    {
        dashSlider.value = 0;
        dashGauge.color = new Color(0.25f, 0.25f, 1.0f);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreChange?.Invoke();
    }

    public void ResetScore(int amount)
    {
        _score = amount;
        OnScoreChange?.Invoke();
    }

    public void SuperMode()
    {
        dashButton.gameObject.SetActive(false);

        UIManager.Instance.ActivateSuperMode();
        EnvironmentSpawner.Instance.ActivateSuperMode();
        SpawnManager.Instance.ActivateSuperMode();

        _gaugeDeplete = true;
    }

    public void SetMaxLife(int amount)
    {
        _life = amount;
        OnLifeChange?.Invoke();
    }

    public void AddLife(int amount)
    {
        _life += amount;
        OnLifeChange?.Invoke();
    }

    public void LoseLife(int amount)
    {
        _life -= amount;
        OnLifeChange?.Invoke();
    }

    public bool GetGaugeState()
    {
        return _gaugeDeplete;
    }

    public void RestartGame()
    {
        UIManager.Instance.StartMenu();
    }
}
