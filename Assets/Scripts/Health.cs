using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public int CurrentHealth
    {
        get => _currentHealth;
    }

    [HideInInspector] public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _currentHealth = Mathf.Max(_currentHealth, 0);

        Debug.Log($"{gameObject.name} took {damage} damage");

        if (_currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} died");
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}
