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

        if (_deathState == false)
        {
            Sound.Instance.DeadEnemy();

            _deathState = true;
            enemyUnit.CallDeath();

            GameManager.Instance.DecreaseSurvivors();
            Destroy(this.gameObject, 2.0f);
        }
    }

    public void KillAnnouncer()
    {
        _killCall = Random.Range(1, 3);

        if (_killCall == 1)
        {
            Sound.Instance.KillAnnouncer(1);
        }
        else if (_killCall == 2)
        {
            Sound.Instance.KillAnnouncer(2);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerPistolBullet player1 = collision.gameObject.GetComponent<PlayerPistolBullet>();
        PlayerRapidFire player2 = collision.gameObject.GetComponent<PlayerRapidFire>();
        PlayerShotgunSpread player3 = collision.gameObject.GetComponent<PlayerShotgunSpread>();

        if (player1 != null || player2 != null || player3 != null)
        {
            GameManager.Instance.IncreaseHighScore();

            TakeDamage(10);

            if (_currentHealth <= 0 && _announcerState == false)
            {
                KillAnnouncer();
                _announcerState = true;

                Death();
            }
        }
    }
}
