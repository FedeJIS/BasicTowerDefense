using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreep : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] protected CreepData data;
    [SerializeField] protected BoxCollider _collider;

    private Renderer _renderer;

    protected MoveTowardsTarget _moveTowardsTarget;

    public Action<float> CreepKilled;

    protected float CurrentHealth;

    private Transform _enemyBase;

    protected void Awake()
    {
        CurrentHealth = data.Health;
        _enemyBase = GameObject.Find("Base").transform;
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsTarget.Target = _enemyBase;
        _moveTowardsTarget.Speed = data.Speed;
        _renderer = GetComponentInChildren<Renderer>();
        GameManager.PlayerWon += (flag) => Destroy(gameObject);
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
            Recycle();
        }
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


    public void Recycle()
    {
        CreepKilled?.Invoke(data.Reward);
        gameObject.SetActive(false);
        transform.position = new Vector3(0, -1000, 0);
        _moveTowardsTarget.enabled = false;
        _collider.enabled = false;
    }

    public void Activate()
    {
        _moveTowardsTarget.enabled = true;
        _collider.enabled = true;
        CurrentHealth = data.Health;
        _renderer.material = data.Material;
        _moveTowardsTarget.Speed = data.Speed;
    }
}
