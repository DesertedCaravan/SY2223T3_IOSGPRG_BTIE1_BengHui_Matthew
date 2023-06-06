using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Singleton<Player>
{
    private bool _kill = false;
    private int startingLife = 3;
    private int _randHealthGain = 0;

    [SerializeField] public Slider slider;
    [SerializeField] private TextMeshProUGUI loseText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetMaxLife(startingLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (_kill == true)
            {
                // Debug.Log(other.name);
                Destroy(other.gameObject);
                SpawnManager.Instance.RemoveEnemyFromList(other.gameObject);
                GameManager.Instance.AddScore(1);

                _kill = false;

                _randHealthGain = Random.Range(0, 100);

                if (_randHealthGain <= 100)
                {
                    GameManager.Instance.AddLife(1);
                    startingLife++;
                }
            }
            else
            {
                GameManager.Instance.LoseLife(1);
                startingLife--;
            }

            if (startingLife <= 0)
            {
                loseText.SetText("GAME OVER");
                Destroy(gameObject);
            }
        }
    }

    public void CorrectSwipe()
    {
        _kill = true;
    }

    public void WrongSwipe()
    {
        _kill = false;
    }

    /*
    private void OnCollisionEnter(Collision collision) // the script is detecting this object type
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject, 0.0f);
        }
    }
    */
}
