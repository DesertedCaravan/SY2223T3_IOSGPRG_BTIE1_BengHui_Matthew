using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI lifeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreChange.AddListener(UpdateScoreDisplay); // No need to change inspector
        UpdateScoreDisplay();

        GameManager.Instance.OnLifeChange.AddListener(UpdateLifeDisplay); // No need to change inspector
        UpdateLifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.SetText(string.Format("Score: {0}", GameManager.Instance._score));
    }

    public void UpdateLifeDisplay()
    {
        lifeDisplay.SetText(string.Format("Life: {0}", GameManager.Instance._life));
    }

}
