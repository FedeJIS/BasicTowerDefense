using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreep : MonoBehaviour, IDamageable
{
    [SerializeField] protected CreepData data;

    protected MoveTowardsTarget _moveTowardsTarget;

    public Action<float> CreepKilled;

    protected float CurrentHealth;

    private Transform _enemyBase;
    
    protected void Start()
    {
        CurrentHealth = data.Health;
        _enemyBase = GameObject.Find("Base").transform;
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsTarget.Target = _enemyBase;
        _moveTowardsTarget.Speed = data.Speed;
    }

    private void OnDestroy()
    {
        CreepKilled = null;
    }

    public float CauseDamage()
    {
        return data.Damage;
    }

    public void ReceiveDamage(float damage)
    {
        Debug.Log(gameObject.name + "received: "+ damage +"of damage.");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CreepKilled?.Invoke(data.Reward);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Creep")) return;
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            ReceiveDamage(damageable.CauseDamage());
        }

        var sideEffect = collision.gameObject.GetComponent<ISideEffect>();
        if (sideEffect != null)
        {
            sideEffect.ApplyEffect(gameObject);
        }
    }


}
