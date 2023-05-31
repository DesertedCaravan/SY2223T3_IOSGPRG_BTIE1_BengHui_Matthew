using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 3.0f;

    public int _health;
    public int _attack;
    public int _defense;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;

        if (transform.position.y == -6.0f)
        {
            Destroy(gameObject);
            SpawnManager.Instance.RemoveEnemyFromList(gameObject);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
    }
    */

    /*
        private void OnCollisionEnter(Collision collision) // the script is detecting this object type
        {
            if (collision.gameObject.tag == "Player")
            {
                SpawnManager.Instance.RemoveEnemyFromList(gameObject);
            }
        }
    */
}