using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBarComponent _healthBarComponent;

    private NexusData _nexusData;
    private float _nexusHealth;

    private static float _coins;

    public static float PlayerCoins => _coins;
    public Action PlayerLost;

    public static Action<float> PlayerRewarded;

    public void SetUpPlayer(NexusData nexusData, int startingCoins)
    {
        _nexusData = nexusData;
        _healthBarComponent.SetMaxValue(_nexusData.NexusHealth);
        _nexusHealth = nexusData.NexusHealth;
        EarnCoins(startingCoins);
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

    public void EarnCoins(float amount)
    {
        _coins += amount;
        PlayerRewarded?.Invoke(_coins);
    }
}
