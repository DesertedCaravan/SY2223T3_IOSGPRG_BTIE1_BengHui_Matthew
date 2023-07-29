using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Image Reference: https://www.bing.com/images/search?view=detailV2&ccid=PDjjFaT4&id=402FB36A53C77E3D235550FF0290F49A3500C1E2&thid=OIP.PDjjFaT4mfOP9VN-5XMZLgHaHa&mediaurl=https%3A%2F%2Fcdn0.iconfinder.com%2Fdata%2Ficons%2Fweapon-5%2F100%2F11-512.png&exph=512&expw=512&q=blast+gun+white+icon&simid=607989828591054779&form=IRPRST&ck=7A3848819180BADB4B1AA552242C3E8A&selectedindex=82&ajaxhist=0&ajaxserp=0&vt=0&sim=11

    // public GameObject explosion;

    public float _speed;
	public float _scatter;
	public float _rate;

    public float _activeTime;

	public virtual void Start()
	{
        _speed = 30f;
	}

	public virtual void Update()
	{
		transform.position += transform.up * _speed * Time.deltaTime;

        _activeTime += 1f * Time.deltaTime;
        
        if (_activeTime >= 4.0f)
        {
            Destroy(this.gameObject);
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        BarrelSpawner barrelSpawner = collision.gameObject.GetComponent<BarrelSpawner>();

        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (barrelSpawner != null)
        {
            Sound.Instance.BarrelBreak(this.transform);
            barrelSpawner.TakeDamage();
            Destroy(this.gameObject);
        }

        if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Wood)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 1);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Stone)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 2);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Metal)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 3);
            Destroy(this.gameObject);
        }
        else if (obstacle != null && obstacle.GetObstacleType() == ObstacleType.Plastic)
        {
            Sound.Instance.ObstacleBulletHit(this.transform, 4);
            Destroy(this.gameObject);
        }
    }
}
