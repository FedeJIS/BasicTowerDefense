using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTurretComponent : MonoBehaviour
{
    [SerializeField] private Button createTurretButton;
    [SerializeField] private TurretData turretData;
    
    public static Action<TurretData> BuildTurretClicked;
    private void Awake()
    {
        createTurretButton.onClick.AddListener(()=> BuildTurretClicked?.Invoke(turretData));
    }
}
