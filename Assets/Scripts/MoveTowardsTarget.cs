using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    private Transform _target;
    private float _speed;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }
    
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    void Update()
    {
        if (!_target) return;
        MoveTowards(_target);
    }
    
    private void MoveTowards(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        var direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
