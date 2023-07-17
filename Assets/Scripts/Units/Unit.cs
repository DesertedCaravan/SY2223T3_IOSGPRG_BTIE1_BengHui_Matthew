using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically adds the Health class to the Game Object
[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    [SerializeField] private string _name;

    /* Original
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    */
    [SerializeField] private Health health;

    [SerializeField] public float _speed;

    public virtual void Initialize(string name, int maxHealth, float speed)
    {
        gameObject.name = name;

        _name = name;

        /* Original
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        */
        health = gameObject.GetComponent<Health>();
        health.MaxHealth = maxHealth;
        health.Initialize();

        _speed = speed;

        Debug.Log($"{_name} has been spawned.");
    }

    public virtual void Fire()
    {
    }
}