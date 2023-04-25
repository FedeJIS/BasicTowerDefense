using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageable
{
    protected Transform _target;
    protected ProjectileData _projectileData;

    public void SetUpProjectile(ProjectileData projectileData)
    {
        _projectileData = projectileData;
    }
    
    public void SetUpTarget(Transform target)
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        _target = target;
        Destroy(gameObject,_projectileData.LifeSpan);
    }
    public float CauseDamage()
    {
        return _projectileData.Damage;
    }

    public void ReceiveDamage(float damage)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(_target) FollowTarget();
    }
    
    private void FollowTarget()
    {
        // Calculate the direction from the current position to the target position
        Vector3 direction = _target.position - transform.position;

        // Normalize the direction vector to have a length of 1
        direction.Normalize();

        // Move towards the target by the moveSpeed amount
        transform.Translate(direction * _projectileData.Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            ReceiveDamage(damageable.CauseDamage());
        }
    }
}
