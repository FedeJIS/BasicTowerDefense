using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private WavesManager wavesManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TurretBuilder turretBuilder;

    public static Action<bool> PlayerWon;
    void Start()
    {
        wavesManager.WavesData = levelData.WaveData;
        wavesManager.WaveFinished += NextWave;
        wavesManager.NoWavesLeft += GameWon;
        wavesManager.CreepKilled += EarnCoins;
        
        playerManager.SetUpPlayer(levelData.NexusData);
        playerManager.PlayerLost += GameLost;

        turretBuilder.TurretPlaced += TakeCoins;
        wavesManager.NextWave();
    }

    void NextWave()
    {
        wavesManager.NextWave();
    }

    void GameLost()
    {
        Debug.Log("Player Lost");
        PlayerWon?.Invoke(false);
    }

    void GameWon()
    {
        Debug.Log("Player Won!");
        PlayerWon?.Invoke(true);
    }

    void EarnCoins(int reward)
    {
        playerManager.EarnCoins(reward);
    }

    void TakeCoins(int amount)
    {
        playerManager.EarnCoins(-amount);
    }
    
}
