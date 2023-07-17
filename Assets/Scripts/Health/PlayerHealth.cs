using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Initialize()
    {
        base.Initialize();
        UIManager.Instance.SetMaxHealth(_maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        UIManager.Instance.SetHealth(_currentHealth);
    }

    public void AddHealth(int health)
    {
        _currentHealth += health;
        _currentHealth = Mathf.Min(_currentHealth, 100);

        UIManager.Instance.SetHealth(_currentHealth);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyPistolBullet enemy1 = collision.gameObject.GetComponent<EnemyPistolBullet>();
        EnemyRapidFire enemy2 = collision.gameObject.GetComponent<EnemyRapidFire>();
        EnemyShotgunSpread enemy3 = collision.gameObject.GetComponent<EnemyShotgunSpread>();

        if (enemy1 != null || enemy2 != null || enemy3 != null)
        {
            TakeDamage(10);

            if (_currentHealth <= 0 && _announcerState == false)
            {
                Sound.Instance.PlayerDeathAnnouncer();
                _announcerState = true;

                PlayerDeath();
            }
        }
    }

    public void PlayerDeath()
    {
        if (_deathState == false)
        {
            Sound.Instance.DeadPlayer();

            _deathState = true;

            GameManager.Instance.GameOver();

            this.gameObject.SetActive(false);
            // Destroy(this.gameObject, 2.0f);
        }
    }
}
