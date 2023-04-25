using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateTurretComponent : MonoBehaviour
{
    [SerializeField] private Button createTurretButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    private TurretData _turretData;
    
    public static Action<TurretData> BuildTurretClicked;

    public TurretData TurretData
    {
        get => _turretData;
        set
        {
            _turretData = value;
            buttonText.text = _turretData.DataName;
        }
    }

    private void Awake()
    {
        createTurretButton.onClick.AddListener(()=> BuildTurretClicked?.Invoke(_turretData));
    }
}
