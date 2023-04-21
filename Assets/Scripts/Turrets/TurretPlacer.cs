using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public Action TurretPlaced;
    private Transform _turretToPlace;

    public void InitializeTurretPlacer(Transform turretToPlace)
    {
        _turretToPlace = turretToPlace;
    }

    private void FollowCursor()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var point  = ray.origin + (ray.direction * Camera.main.transform.position.y);
        _turretToPlace.position = point;
    }
    private void Update()
    {
        if (!_turretToPlace) return;

        FollowCursor();

        if (Input.GetMouseButtonDown(0))
        {
            TurretPlaced?.Invoke();
            Destroy(gameObject);
        }
    }
}
