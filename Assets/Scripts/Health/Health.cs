using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public int CurrentHealth
    {
        get => _currentHealth;
    }

    [HideInInspector]
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] public int _currentHealth;
    [SerializeField] public int _maxHealth;

    public int _killCall = 1;

    public bool _announcerState = false;
    public bool _deathState = false;

    public virtual void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log($"{gameObject.name} took {damage} damage");
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }

    public void Death()
    {
        EnemyMovement enemyUnit = this.gameObject.GetComponent<EnemyMovement>();
        BossPickupSpawner bossPickupSpawner = this.gameObject.GetComponent<BossPickupSpawner>();

        if (_deathState == false)
        {
            Sound.Instance.DeadEnemy(this.transform);

            _deathState = true;
            enemyUnit.CallDeath();

            GameManager.Instance.DecreaseSurvivors();

            if (bossPickupSpawner != null)
            {
                bossPickupSpawner.SpawnBossDrops();
            }

            Destroy(this.gameObject, 2.0f);
        }
    }

    public void KillAnnouncer()
    {
        _killCall = Random.Range(1, 3);

        if (_killCall == 1)
        {
            Sound.Instance.KillAnnouncer(this.transform, 1);
        }
        else if (_killCall == 2)
        {
            Sound.Instance.KillAnnouncer(this.transform, 2);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        GrenadeExplosion grenadeExplosion = collision.gameObject.GetComponent<GrenadeExplosion>();

        if (bullet != null)
        {
            if (bullet.GetBulletType() == BulletType.PlayerPistolBullet || bullet.GetBulletType() == BulletType.PlayerShotgunSpread)
            {
                GameManager.Instance.IncreaseHighScore();
                TakeDamage(10);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }
            else if (bullet.GetBulletType() == BulletType.PlayerRapidFire)
            {
                GameManager.Instance.IncreaseHighScore();
                TakeDamage(15);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }

            if (bullet.GetBulletType() == BulletType.EnemyPistolBullet || bullet.GetBulletType() == BulletType.EnemyShotgunSpread)
            {
                TakeDamage(10);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }
            else if (bullet.GetBulletType() == BulletType.EnemyRapidFire)
            {
                TakeDamage(15);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }

            if (_currentHealth <= 0 && _announcerState == false)
            {
                KillAnnouncer();
                _announcerState = true;

                Death();
            }
        }

        if (grenadeExplosion != null)
        {
            if (grenadeExplosion.GetExplosionType() == ExplosionType.PlayerExplosion)
            {
                GameManager.Instance.IncreaseHighScore();
                TakeDamage(100);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }

            if (grenadeExplosion.GetExplosionType() == ExplosionType.EnemyExplosion)
            {
                TakeDamage(100);

                if (_currentHealth > 0)
                {
                    Sound.Instance.HurtEnemy(this.transform);
                }
            }

            if (_currentHealth <= 0 && _announcerState == false)
            {
                KillAnnouncer();
                _announcerState = true;

                Death();
            }
        }
    }
}
