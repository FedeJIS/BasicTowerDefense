using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private Transform spawnPoint;
    
    private Transform projectileSpawnPool;

    private BaseCreep _aimTarget;
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
            yield return new WaitForSeconds(projectileData.Cadence);
            if (_aimTarget == null) yield return null;
            
            var projectile = _projectilePool.Get(projectileData.Id).Item2.GetComponent<Projectile>();
            projectile.transform.position = spawnPoint.position;
            projectile.transform.parent = projectileSpawnPool;
            projectile.Activate();
            projectile.SetUpProjectile(projectileData);
            projectile.OnDeactivation ??= CleanTargets;

            if (_aimTarget != null)
            {
                projectile.SetUpTarget(_aimTarget.transform);
            }
            else
            {
                projectile.Recycle();
            }

        }
        
        Destroy(gameObject);
    }

    private void CleanTargets()
    {
        if(!_aimTarget) return;
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
        if(collision.gameObject.GetComponent<BaseTurret>()) return;
        if (!_turretPlaced) return;
        if (_aimTarget != null) return;
        
        StopAllCoroutines();
        _aimTarget = collision.gameObject.GetComponent<BaseCreep>();
        _aimTarget.CreepKilled ??= (reward) => CleanTargets();
        Shoot();
    }
    private void OnCollisionExit(Collision other)
    {
        if (_aimTarget == null) return;
        if (other.gameObject == _aimTarget.gameObject) CleanTargets();
    }
}
