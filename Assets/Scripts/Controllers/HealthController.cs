using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController
{
    private float _health;
    private float _maxHealth;

    private Action _callback;
    public float Health => _health;
    public HealthController(float maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        _health -= amount;
        
        if(_health <= 0) _callback?.Invoke();
    }

    public void Heal()
    {
        _health = _maxHealth;
    }

    public void OnReachedZero(Action callback)
    {
        _callback = callback;
    }
 
}
