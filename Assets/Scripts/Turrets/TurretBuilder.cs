using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject turretButtonPrefab;
    private GenericFactory<TurretData> _turretFactory;

    public Action<int> TurretPlaced;
    private const string DataPath = "ScriptableData/TurretsData";

    private Transform _turretContainer;
    public void Initialize()
    {
        
        CreateTurretComponent.BuildTurretClicked += BuildTurret;

        _turretFactory = new GenericFactory<TurretData>(DataPath);
        
        DisplayTurretButtons();
        
        _turretContainer = new GameObject("Turret Container").transform;
        
        _turretContainer.SetParent(transform);
        
    }

    private void DisplayTurretButtons()
    {
        foreach (var turretData in _turretFactory.GetAllData())
        {
            var turretButton = Instantiate(turretButtonPrefab, parent).GetComponent<CreateTurretComponent>();
            turretButton.TurretData = turretData;
        }
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
        if (PlayerManager.PlayerCoins < selectedTurret.BuildCost) return;
        
        //Create Turret to Place
        var turretToPlace = _turretFactory.FabricateById(selectedTurret.Id);

        if (turretToPlace == null) return;

        var turret = turretToPlace.Item2.GetComponent<BaseTurret>();
        
        turret.transform.SetParent(_turretContainer);

        if (!turret) return;
        
        //Create Turret Placer
        var turretPlacerGo = new GameObject("Turret Placer");
        
        turretPlacerGo.transform.SetParent(transform);
        
        turretPlacerGo.AddComponent<TurretPlacer>();

        var turretPlacer = turretPlacerGo.GetComponent<TurretPlacer>();

        turretPlacer.InitializeTurretPlacer(turret.transform);
        
        //Place Turret
        turretPlacer.TurretPlaced += () =>
        {
            turret.Defend();
            TurretPlaced?.Invoke(selectedTurret.BuildCost);
        };

    }
}
