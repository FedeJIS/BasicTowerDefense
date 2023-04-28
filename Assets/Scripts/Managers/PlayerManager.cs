using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBarComponent healthBarComponent;

    private NexusData _nexusData;
    public static float PlayerCoins => _coinsController.GetCoins();

    public Action PlayerLost;

    public static Action<float> PlayerRewarded;

    private HealthController _healthController;
    
    private static CoinsController _coinsController;

    public void SetUpPlayer(NexusData nexusData, int startingCoins)
    {
        _nexusData = nexusData;
        _coinsController = new CoinsController();
        _healthController = new HealthController(_nexusData.NexusHealth);
        _healthController.OnReachedZero(() => PlayerLost?.Invoke());
        healthBarComponent.SetMaxValue(_nexusData.NexusHealth);
        UpdateCoins(startingCoins);
    }

    public float CauseDamage()
    {
        return float.MaxValue;
    }

    public void ReceiveDamage(float damage)
    {
        _healthController.TakeDamage(damage);
        healthBarComponent.UpdateHealth(_healthController.Health);
    }
    
    public void UpdateCoins(float amount)
    {
        _coinsController.UpdateCoins(amount);
        PlayerRewarded?.Invoke(_coinsController.GetCoins());
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
