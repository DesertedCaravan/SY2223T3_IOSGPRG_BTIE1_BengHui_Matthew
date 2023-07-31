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
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        GrenadeExplosion grenadeExplosion = collision.gameObject.GetComponent<GrenadeExplosion>();

        if (bullet != null)
        {
            if (bullet.GetBulletType() == BulletType.EnemyPistolBullet || bullet.GetBulletType() == BulletType.EnemyShotgunSpread)
            {
                TakeDamage(10);
            }
            else if (bullet.GetBulletType() == BulletType.EnemyRapidFire)
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

        if (grenadeExplosion != null)
        {
            TakeDamage(100);

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
