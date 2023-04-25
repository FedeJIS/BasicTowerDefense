using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{
    [SerializeField]  private Slider _healthBar;
    
    public void TakeDamage(float damage)
    {
        _healthBar.value = (_healthBar.value - damage) / _healthBar.maxValue;
    }
    
}
