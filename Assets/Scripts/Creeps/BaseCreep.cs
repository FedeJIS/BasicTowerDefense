using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreep : MonoBehaviour, IDamageable
{
    [SerializeField] protected CreepData data;
    
    public Action CreepKilled;

    protected float CurrentHealth;

    private Transform _enemyBase;

    protected void Start()
    {
        CurrentHealth = data.Health;
        _enemyBase = GameObject.Find("Base").transform;
    }


    private void MoveTowardsBase()
    {
        
    }

    public float CauseDamage()
    {
        return data.Damage;
    }

    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CreepKilled?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            ReceiveDamage(damageable.CauseDamage());
        }
    }

    protected void Update()
    {
        MoveTowardsTarget(_enemyBase);
    }

    protected void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemyBase.position, data.Speed * Time.deltaTime);
        
        var direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
