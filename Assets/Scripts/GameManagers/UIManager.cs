using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI characterSelectText;

    [SerializeField] private TextMeshProUGUI lifeDisplay;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Slider dashSlider;
    [SerializeField] private Button dashButton;

    [SerializeField] private Player player;
    [SerializeField] private Button retryButton;

    public bool _gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu();

        // No need to change inspector
        GameManager.Instance.OnScoreChange.AddListener(UpdateScoreDisplay);
        UpdateScoreDisplay();

        GameManager.Instance.OnLifeChange.AddListener(UpdateLifeDisplay);
        UpdateLifeDisplay();
    }

    public void StartMenu()
    {
        characterSelectText.gameObject.SetActive(true);

        lifeDisplay.gameObject.SetActive(false);
        scoreDisplay.gameObject.SetActive(false);
        eventText.gameObject.SetActive(false);
        arrowText.gameObject.SetActive(false);
        arrowImage.gameObject.SetActive(false);
        dashSlider.gameObject.SetActive(false);
        dashButton.gameObject.SetActive(false);

        retryButton.gameObject.SetActive(false);
    }
    
    public bool GetGameState()
    {
        return _gameStart;
    }

    public void DefaultSelected()
    {
        Player.Instance.StartGame(0);
    }

    public void TankSelected()
    {
        Player.Instance.StartGame(1);
    }

    public void SpeedSelected()
    {
        Player.Instance.StartGame(2);
    }

    public void StartGame()
    {
        characterSelectText.gameObject.SetActive(false);
        
        lifeDisplay.gameObject.SetActive(true);
        scoreDisplay.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        arrowText.gameObject.SetActive(true);
        arrowImage.gameObject.SetActive(true);

        dashSlider.gameObject.SetActive(true);
        dashButton.gameObject.SetActive(false);

        player.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);

        StartCoroutine(CO_ReadyText());

        GameManager.Instance.ResetScore(0);
        GameManager.Instance.RestartGauge();

        SpawnManager.Instance.GameOn();
        EnvironmentSpawner.Instance.GameOn();
        ArrowManager.Instance.GameOn();

        SpawnManager.Instance.GameStart();
        EnvironmentSpawner.Instance.GameStart();
        ArrowManager.Instance.GameStart();

        _gameStart = true;
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.SetText(string.Format("Score: {0}", GameManager.Instance._score));
    }

    public void UpdateLifeDisplay()
    {
        lifeDisplay.SetText(string.Format("Life: {0}", GameManager.Instance._life));
    }

    public void RevealArrow()
    {
        arrowImage.gameObject.SetActive(true);
    }
    public void HideArrow()
    {
        arrowImage.gameObject.SetActive(false);
    }

    public void LifeGain()
    {
        StartCoroutine(CO_LifeGainText());
    }

    public void ActivateSuperMode()
    {
        Player.Instance.SuperModeOn();
        StartCoroutine(CO_SuperModeText());
    }

    public void DeactivateSuperMode()
    {
        StartCoroutine(CO_DeactivateDelay());
    }

    public void GameOverScreen()
    {
        _gameStart = false;

        arrowText.gameObject.SetActive(false);

        SpawnManager.Instance.GamePause();
        EnvironmentSpawner.Instance.GamePause();
        ArrowManager.Instance.GamePause();

        StartCoroutine(CO_ResetDelay());
    }

    private IEnumerator CO_ReadyText()
    {
        eventText.SetText("Get Ready...");
        yield return new WaitForSeconds(2.0f);
        eventText.SetText("");
    }

    private IEnumerator CO_LifeGainText()
    {
        eventText.SetText("Life Gained!");
        yield return new WaitForSeconds(2.0f);
        eventText.SetText("");
    }

    private IEnumerator CO_SuperModeText()
    {
        eventText.SetText("Dash Activated!");
        yield return new WaitForSeconds(3.0f);
        eventText.SetText("");
    }

    private IEnumerator CO_DeactivateDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Player.Instance.SuperModeOff();
    }

    private IEnumerator CO_ResetDelay()
    {
        eventText.SetText("GAME OVER");
        yield return new WaitForSeconds(4.0f);
        retryButton.gameObject.SetActive(true);
    }
}