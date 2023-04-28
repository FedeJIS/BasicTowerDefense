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
    public static Action<WaveData> WaveStarted;
    public static bool IsGameOver;
    
    public const float WaitTime = 3f;
    
    void Start()
    {
        //Waves Manager Events
        wavesManager.WavesData = levelData.WaveData;
        wavesManager.WaveFinished += NextWave;
        wavesManager.NoWavesLeft += GameWon;
        wavesManager.CreepKilled += EarnCoins;
        wavesManager.WaveStarted += WaveHasStarted;

        //Player Manager Events + Setup
        playerManager.SetUpPlayer(levelData.NexusData,levelData.StartingCoins);
        playerManager.PlayerLost += GameLost;

        //Turret Builder Events
        turretBuilder.TurretPlaced += TakeCoins;
        
        //Start First Wave
        NextWave();
    }

    void NextWave()
    {
        StartCoroutine(Rest());
    }

    void WaveHasStarted(WaveData waveData)
    {
        WaveStarted?.Invoke(waveData);
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(WaitTime);
        wavesManager.NextWave();
    }

    void GameLost()
    {
        IsGameOver = true;
        PlayerWon?.Invoke(false);
    }

    void GameWon()
    {
        IsGameOver = true;
        PlayerWon?.Invoke(true);
    }

    void EarnCoins(float reward)
    {
        playerManager.UpdateCoins(reward);
    }

    void TakeCoins(int amount)
    {
        playerManager.UpdateCoins(-amount);
    }
    
}
