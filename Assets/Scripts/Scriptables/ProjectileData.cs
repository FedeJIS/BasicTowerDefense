using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Projectiles/New Projectile Data", order = 0, fileName = "New Projectile Data")]
public class ProjectileData : ScriptableData
{
   [SerializeField] private float cadence;
   [SerializeField] private float damage;
   [SerializeField] private float speed;
   [SerializeField] private float lifeSpan;
   public float Cadence => cadence;
   public float Damage => damage;
   public float Speed => speed;

   public float LifeSpan => lifeSpan;
}
