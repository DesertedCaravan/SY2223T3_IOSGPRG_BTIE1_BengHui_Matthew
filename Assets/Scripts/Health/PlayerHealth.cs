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
            if (enemy1 != null || enemy3 != null)
            {
                TakeDamage(10);
            }
            else if (enemy2 != null)
            {
                TakeDamage(15);
            }

            if (_currentHealth > 0)
            {
                Sound.Instance.HurtPlayer();
            }

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
            PlayerMovement move = this.gameObject.GetComponent<PlayerMovement>();
            PlayerAim aim = this.gameObject.GetComponent<PlayerAim>();

            Sound.Instance.DeadPlayer();

            move.CallDeath();
            aim.CallDeath();

            _deathState = true;

            GameManager.Instance.GameOver();
            UIManager.Instance.GameOver();

            StartCoroutine(CO_SetDefeat());
        }
    }

    IEnumerator CO_SetDefeat()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        this.gameObject.SetActive(false);
    }
}
