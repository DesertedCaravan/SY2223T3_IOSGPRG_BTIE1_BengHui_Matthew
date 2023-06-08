using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 3.0f;

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;

        if (transform.position.y <= -6.0f)
        {
            Destroy(gameObject);
            SpawnManager.Instance.RemoveEnemyFromList(gameObject);
        }
    }

    public void ActivateSuperMode()
    {
        speed += 10.0f;
    }

    public void DeactivateSuperMode()
    {
        speed -= 10.0f;
    }
}