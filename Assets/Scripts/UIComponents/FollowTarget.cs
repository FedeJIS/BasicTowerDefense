using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform target;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    private void Update()
    {
        if (target == null) return;
        transform.position = target.position + offset;
    }
}
