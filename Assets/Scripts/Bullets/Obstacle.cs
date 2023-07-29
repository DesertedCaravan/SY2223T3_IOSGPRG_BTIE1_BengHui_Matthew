using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Wood,
    Stone,
    Metal,
    Plastic
}

public class Obstacle : MonoBehaviour
{
    [SerializeField] public ObstacleType _obstacleType;

    public ObstacleType GetObstacleType()
    {
        return _obstacleType;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            Destroy(collision.gameObject);
        }
    }
}