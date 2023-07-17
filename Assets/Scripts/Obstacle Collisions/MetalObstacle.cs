using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalObstacle : Obstacle
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            // Sound.Instance.ObstacleBulletHit(audioSource, 3);
        }
    }
}