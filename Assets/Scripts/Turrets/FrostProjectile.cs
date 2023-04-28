using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostProjectile : Projectile, ISideEffect
{
    [SerializeField] private Material frostMaterial;
    private const int EffectTime = 3;
    public void ApplyEffect(GameObject other)
    {
        var moveTowards = other.GetComponent<MoveTowardsTarget>();
        
        if(!moveTowards) return;
        
        if (!gameObject.activeSelf) return;
        
        StartCoroutine(SideEffectCoroutine(moveTowards));

    }

    private IEnumerator SideEffectCoroutine(MoveTowardsTarget moveTowards)
    {
        var currentSpeed = moveTowards.Speed;

        moveTowards.Speed /= 2;
        
        Renderer renderer = moveTowards.GetComponentInChildren<MeshRenderer>();

        var currentMaterial = renderer.material;
        
        renderer.material = frostMaterial;

        yield return new WaitForSeconds(EffectTime);

        moveTowards.Speed = currentSpeed;
        
        renderer.material = currentMaterial;
    }
}
