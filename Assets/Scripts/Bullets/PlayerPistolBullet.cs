using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPistolBullet : Bullet
{
	public override void Start()
	{
		_speed = 30f;
		_scatter = Random.Range(-0.6f, 0.6f);
	}

	public override void Update()
	{
		transform.position += transform.right * _scatter * Time.deltaTime;
		transform.position += transform.up * _speed * Time.deltaTime;

		_activeTime += 1f * Time.deltaTime;

		if (_activeTime >= 4.0f)
		{
			Destroy(this.gameObject);
		}
	}
}
