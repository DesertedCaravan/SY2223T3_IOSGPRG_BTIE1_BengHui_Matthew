using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Image Reference: https://www.bing.com/images/search?view=detailV2&ccid=PDjjFaT4&id=402FB36A53C77E3D235550FF0290F49A3500C1E2&thid=OIP.PDjjFaT4mfOP9VN-5XMZLgHaHa&mediaurl=https%3A%2F%2Fcdn0.iconfinder.com%2Fdata%2Ficons%2Fweapon-5%2F100%2F11-512.png&exph=512&expw=512&q=blast+gun+white+icon&simid=607989828591054779&form=IRPRST&ck=7A3848819180BADB4B1AA552242C3E8A&selectedindex=82&ajaxhist=0&ajaxserp=0&vt=0&sim=11

	// public GameObject explosion;

	public float _rate;
	public float _speed = 10f;

	void Update()
	{
		transform.position += transform.up * _speed * Time.deltaTime;
	}

    public void Initialize(float speed, float rate)
    {
		_rate = rate;
		_speed = speed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
	{
		// Only possible if the Game Object that collides with Player has the Health class attached to it.
		Health health = collision.gameObject.GetComponent<Health>();
		BarrelSpawner barrelSpawner = collision.gameObject.GetComponent<BarrelSpawner>();

		if (health != null)
		{
			Sound.Instance.HurtEnemy();
			health.TakeDamage(10);
		}

		if (barrelSpawner != null)
		{
			Sound.Instance.HurtEnemy();
			barrelSpawner.TakeDamage();
		}

		// GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
		// Destroy(e, 1.5f);
		Destroy(this.gameObject);
	}
}
