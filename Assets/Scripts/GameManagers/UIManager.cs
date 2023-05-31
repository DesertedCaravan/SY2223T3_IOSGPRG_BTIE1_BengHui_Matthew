using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreChange.AddListener(UpdateScoreDisplay); // No need to change inspector
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.SetText(string.Format("Score: {0}", GameManager.Instance._score));
    }

}
