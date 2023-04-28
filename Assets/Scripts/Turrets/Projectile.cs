using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] protected SphereCollider _collider;
    
    protected Transform _target;
    protected ProjectileData _projectileData;

    public Action OnDeactivation;
 

    public void SetUpProjectile(ProjectileData projectileData)
    {
        _projectileData = projectileData;
    }
    
    public void SetUpTarget(Transform target)
    {
        if (target == null)
        {
            Recycle();
            return;
        }
        _target = target;
        StartCoroutine(RecycleInTime(_projectileData.LifeSpan));
    }
    public float CauseDamage()
    {
        return _projectileData.Damage;
    }

    public void ReceiveDamage(float damage)
    {
        Recycle();
    }

    protected void Update()
    {
        if(_target) FollowTarget();
    }
    
    protected void FollowTarget()
    {
        // Calculate the direction from the current position to the target position
        Vector3 direction = _target.position - transform.position;

        // Normalize the direction vector to have a length of 1
        direction.Normalize();

        // Move towards the target by the moveSpeed amount
        transform.Translate(direction * _projectileData.Speed * Time.deltaTime);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            ReceiveDamage(damageable.CauseDamage());
        }
    }

    public virtual void Recycle()
    {
        _target = null;
        OnDeactivation?.Invoke();
        _collider.enabled = false;
        transform.position = new Vector3(0, -2000, 0);
        gameObject.SetActive(false);
       
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    protected IEnumerator RecycleInTime(float timeToRecycle)
    {
        yield return new WaitForSeconds(timeToRecycle);
        Recycle();
    }
}
