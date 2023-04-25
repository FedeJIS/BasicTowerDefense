using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBarComponent _healthBarComponent;

    private NexusData _nexusData;
    private float _nexusHealth;

    public Action PlayerLost;


    public void SetUpPlayer(NexusData nexusData)
    {
        _nexusData = nexusData;
        _healthBarComponent.SetMaxValue(_nexusData.NexusHealth);
        _nexusHealth = nexusData.NexusHealth;
    }


    public float CauseDamage()
    {
        return float.MaxValue;
    }

    public void ReceiveDamage(float damage)
    {
        _nexusHealth -= damage;
        _healthBarComponent.UpdateHealth(_nexusHealth);

        if (_nexusHealth <= 0)
        {
            PlayerLost?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            ReceiveDamage(damageable.CauseDamage());
        }
    }
}
