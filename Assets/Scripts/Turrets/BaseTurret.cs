using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private Transform spawnPoint;
    
    private Transform projectileSpawnPool;

    private Transform _aimTarget;
    private bool _turretPlaced;

    private GenericPool<ProjectileData> _projectilePool;

    private string dataPath = "ScriptableData/ProjectilesData";

    private void Awake()
    {
        _projectilePool = new GenericPool<ProjectileData>(dataPath);
        projectileSpawnPool = GameObject.FindGameObjectWithTag("ProjectilePool").transform;
    }

    public void Defend()
    {
        _turretPlaced = true;
        Debug.Log(name + " started to defend");
    }

    private void Shoot()
    {
        StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        while (!GameManager.IsGameOver)
        {
            if (_aimTarget == null) yield return null;
            yield return new WaitForSeconds(projectileData.Cadence);
            
            var projectile = _projectilePool.Get(projectileData.Id).Item2.GetComponent<Projectile>();
           
            projectile.transform.position = spawnPoint.position;
            projectile.transform.parent = projectileSpawnPool;
            projectile.Activate();
            projectile.SetUpProjectile(projectileData);
            projectile.SetUpTarget(_aimTarget);

            projectile.OnDeactivation ??= CleanTargets;
          
        }
        
        Destroy(gameObject);
    }

    private void CleanTargets()
    {
        _aimTarget = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
       ShootOnlyAtTarget(collision);
    }
    
    private void OnCollisionStay(Collision collision)
    {
        ShootOnlyAtTarget(collision);
    }

    private void ShootOnlyAtTarget(Collision collision)
    {
        if (!_turretPlaced) return;
        if (_aimTarget != null) return;
        
        StopAllCoroutines();
        _aimTarget = collision.transform;
        Shoot();
    }
    private void OnCollisionExit(Collision other)
    {
        if (_aimTarget == null) return;
        if (other.gameObject == _aimTarget.gameObject) _aimTarget = null;
    }
}
