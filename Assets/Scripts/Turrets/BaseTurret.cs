using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] protected ProjectileData projectileData;
    [SerializeField] protected Transform spawnPoint;

    protected Transform _aimTarget;
    protected bool _turretPlaced;

    public void Defend()
    {
        _turretPlaced = true;
        Debug.Log(name + " started to defend");
    }

    protected void Shoot()
    {
        StartCoroutine(ShootingCoroutine());
    }

    protected IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            if (_aimTarget == null) yield return null;
            var projectile = Instantiate(projectileData.Prefab, spawnPoint).GetComponent<Projectile>();
            projectile.SetUpProjectile(projectileData);
            projectile.SetUpTarget(_aimTarget);
            yield return new WaitForSeconds(projectileData.Cadence);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
       ShootOnlyAtTarget(collision);
    }
    
    protected void OnCollisionStay(Collision collision)
    {
        ShootOnlyAtTarget(collision);
    }

    protected void ShootOnlyAtTarget(Collision collision)
    {
        if (!_turretPlaced) return;
        if (_aimTarget != null) return;
        
        StopAllCoroutines();
        _aimTarget = collision.transform;
        Shoot();
    }
    protected void OnCollisionExit(Collision other)
    {
        if (_aimTarget == null) return;
        if (other.gameObject == _aimTarget.gameObject) _aimTarget = null;
    }
}
