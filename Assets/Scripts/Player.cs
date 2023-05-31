using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Singleton<Player>
{
    private bool _kill = false;

    [SerializeField] private TextMeshProUGUI loseText;

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            else
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
