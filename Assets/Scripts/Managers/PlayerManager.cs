using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour, IDamageable, IWallet
{
    [SerializeField] private HealthBarComponent healthBarComponent;

    private NexusData _nexusData;
    private float _nexusHealth;

    private static float _coins;
    
    public static float PlayerCoins => _coins;
    public Action PlayerLost;

    public static Action<float> PlayerRewarded;

    public void SetUpPlayer(NexusData nexusData, int startingCoins)
    {
        _nexusData = nexusData;
        _nexusHealth = nexusData.NexusHealth;
        healthBarComponent.SetMaxValue(_nexusData.NexusHealth);
        UpdateCoins(startingCoins);
    }

    public float CauseDamage()
    {
        return float.MaxValue;
    }

    public void ReceiveDamage(float damage)
    {
        _nexusHealth -= damage;
        healthBarComponent.UpdateHealth(_nexusHealth);

        if (_nexusHealth <= 0)
        {
            PlayerLost?.Invoke();
        }
    }
    
    public void UpdateCoins(float amount)
    {
        _coins += amount;
        PlayerRewarded?.Invoke(_coins);
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
