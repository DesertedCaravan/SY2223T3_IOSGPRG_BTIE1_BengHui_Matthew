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
    [SerializeField] private Image arrow;
    [SerializeField] private Slider dashSlider;

    [SerializeField] private Player player;
    [SerializeField] private Button retryButton;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu();

        GameManager.Instance.OnScoreChange.AddListener(UpdateScoreDisplay); // No need to change inspector
        UpdateScoreDisplay();

        GameManager.Instance.OnLifeChange.AddListener(UpdateLifeDisplay); // No need to change inspector
        UpdateLifeDisplay();
    }

    public void StartMenu()
    {
        characterSelectText.gameObject.SetActive(true);

        lifeDisplay.gameObject.SetActive(false);
        scoreDisplay.gameObject.SetActive(false);
        eventText.gameObject.SetActive(false);
        arrowText.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        dashSlider.gameObject.SetActive(false);

        retryButton.gameObject.SetActive(false);
    }

    public void DefaultSelected()
    {
        Player.Instance.StartingDefault();
    }

    public void TankSelected()
    {
        Player.Instance.StartingTank();
    }

    public void SpeedSelected()
    {
        Player.Instance.StartingSpeed();
    }

    public void StartGame()
    {
        characterSelectText.gameObject.SetActive(false);
        
        lifeDisplay.gameObject.SetActive(true);
        scoreDisplay.gameObject.SetActive(true);
        eventText.gameObject.SetActive(true);
        arrowText.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        dashSlider.gameObject.SetActive(true);

        player.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);

        StartCoroutine(CO_ReadyText());

        GameManager.Instance.ResetScore(0);
        GameManager.Instance.RestartGauge();

        SpawnManager.Instance.GameOn();
        EnvironmentSpawner.Instance.GameOn();
        ArrowManager.Instance.GameOn();

        SpawnManager.Instance.CorouteOn();
        EnvironmentSpawner.Instance.CorouteOn();
        ArrowManager.Instance.CorouteOn();
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.SetText(string.Format("Score: {0}", GameManager.Instance._score));
    }

    public void UpdateLifeDisplay()
    {
        lifeDisplay.SetText(string.Format("Life: {0}", GameManager.Instance._life));
    }

    public void GameOverScreen()
    {
        SpawnManager.Instance.CoroutePause();
        EnvironmentSpawner.Instance.CoroutePause();
        ArrowManager.Instance.CoroutePause();

        SpawnManager.Instance.RemoveAllEnemies();

        StartCoroutine(CO_ResetDelay());
    }

    private IEnumerator CO_ReadyText()
    {
        eventText.SetText("Get Ready...");
        yield return new WaitForSeconds(2.0f);
        eventText.SetText("");
    }

    private IEnumerator CO_ResetDelay()
    {
        eventText.SetText("GAME OVER");
        yield return new WaitForSeconds(4.0f);
        retryButton.gameObject.SetActive(true);
    }
}