using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreep : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] protected CreepData data;
    [SerializeField] protected BoxCollider _collider;

    public Action<float> CreepKilled;

    private Renderer _renderer;
    private Transform _enemyBase;

    private MoveTowardsTarget _moveTowardsTarget;
    private HealthController _healthController;

    private void Awake()
    {
        _enemyBase = GameObject.Find("Base").transform;
        _renderer = GetComponentInChildren<Renderer>();
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsTarget.Target = _enemyBase;
        _moveTowardsTarget.Speed = data.Speed;
        _healthController = new HealthController(data.Health);
        _healthController.OnReachedZero(Recycle);
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
        _healthController.TakeDamage(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BaseCreep>()) return;
        
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
        gameObject.SetActive(false);
        transform.position = new Vector3(0, -1000, 0);
        _moveTowardsTarget.enabled = false;
        _collider.enabled = false;
        CreepKilled?.Invoke(data.Reward);
    }

    public void Activate()
    {
        _moveTowardsTarget.enabled = true;
        _collider.enabled = true;
        _healthController.Heal();
        _renderer.material = data.Material;
        _moveTowardsTarget.Speed = data.Speed;
    }
}
