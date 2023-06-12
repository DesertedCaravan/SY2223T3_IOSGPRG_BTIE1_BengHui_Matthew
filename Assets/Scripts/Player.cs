using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
    [SerializeField] private TextMeshProUGUI eventText;

    public AudioSource audioSource;
    public AudioClip destroyEnemy;
    public AudioClip hurt;
    public AudioClip powerUp;

    private bool _kill = false;
    private bool _superMode = false;
    private int _startingLife = 3;
    private int _randHealthGain = 0;
    private int _gaugeFill = 5;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (_kill == true || _superMode == true)
            {
                audioSource.PlayOneShot(destroyEnemy);

                Destroy(other.gameObject);
                SpawnManager.Instance.RemoveEnemyFromList(other.gameObject);

                GameManager.Instance.AddScore(1);

                if (_superMode == false)
                {
                    GameManager.Instance.AddGauge(_gaugeFill);
                }

                _randHealthGain = Random.Range(0, 100);

                if (_randHealthGain <= 2)
                {
                    audioSource.PlayOneShot(powerUp);
                    UIManager.Instance.LifeGain();
                    GameManager.Instance.AddLife(1);
                    _startingLife++;
                }

                _kill = false;
            }
            else
            {
                audioSource.PlayOneShot(hurt);
                GameManager.Instance.LoseLife(1);
                _startingLife--;
            }

            if (_startingLife <= 0)
            {
                audioSource.PlayOneShot(hurt);
                gameObject.SetActive(false);
                UIManager.Instance.GameOverScreen();
            }
        }
    }

    public void StartGame(int selection)
    {
        _startingLife = 3;
        _gaugeFill = 5;

        if (selection == 1)
        {
            _startingLife = 5;
        }
        else if (selection == 2)
        {
            _gaugeFill = 10;
        }

        UIManager.Instance.StartGame();
        GameManager.Instance.SetMaxLife(_startingLife);
    }

    public void CorrectSwipe()
    {
        _kill = true;
    }

    public void WrongSwipe()
    {
        _kill = false;
    }

    public void SuperModeOn()
    {
        _superMode = true;
    }

    public void SuperModeOff()
    {
        _superMode = false;
    }
}
