using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{
    [SerializeField]  private Slider _healthBar;

    public void SetMaxValue(float maxValue)
    {
        _healthBar.maxValue = maxValue;
        _healthBar.value = maxValue;
    }
    public void UpdateHealth(float health)
    {
        _healthBar.value = health;
    }
    
}
