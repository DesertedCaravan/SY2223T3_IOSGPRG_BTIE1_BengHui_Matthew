using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    // Event
    public UnityEvent OnScoreChange;
    public UnityEvent OnLifeChange;

    public int _score = 0;
    public int _life = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreChange?.Invoke();
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
}
