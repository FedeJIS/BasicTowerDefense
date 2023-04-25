using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour, IInitializable
{
    private GenericFactory<TurretData> _turretFactory;
    
    
    private const string dataPath = "ScriptableData/TurretsData";

    private Transform _turretContainer;
    public void Initialize()
    {
        CreateTurretComponent.BuildTurretClicked += BuildTurret;

        _turretFactory = new GenericFactory<TurretData>(dataPath);
        
        _turretContainer = new GameObject("Turret Container").transform;
        
        _turretContainer.SetParent(transform);
        
        
        
        
    }

    private void OnDestroy()
    {
        CreateTurretComponent.BuildTurretClicked -= BuildTurret;
    }

    private void Awake()
    {
        Initialize();
    }
   

    private void BuildTurret(TurretData selectedTurret)
    {
        //Create Turret to Place
        var turretToPlace = _turretFactory.FabricateById(selectedTurret.Id);

        if (turretToPlace == null) return;

        var turret = turretToPlace.GetComponent<BaseTurret>();
        
        turret.transform.SetParent(_turretContainer);

        if (!turret) return;
        
        //Create Turret Placer
        var turretPlacerGo = new GameObject("Turret Placer");
        
        turretPlacerGo.transform.SetParent(transform);
        
        turretPlacerGo.AddComponent<TurretPlacer>();

        var turretPlacer = turretPlacerGo.GetComponent<TurretPlacer>();

        turretPlacer.InitializeTurretPlacer(turretToPlace.transform);
        
        turretPlacer.TurretPlaced += () => turret.Defend();

    }
    

  
}
