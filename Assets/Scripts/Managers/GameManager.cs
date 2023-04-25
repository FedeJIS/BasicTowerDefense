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
    
    public const float WaitTime = 3f;
    
    void Start()
    {
        wavesManager.WavesData = levelData.WaveData;
        wavesManager.WaveFinished += NextWave;
        wavesManager.NoWavesLeft += GameWon;
        wavesManager.CreepKilled += EarnCoins;
        wavesManager.WaveStarted += WaveHasStarted;

        playerManager.SetUpPlayer(levelData.NexusData,levelData.StartingCoins);
        playerManager.PlayerLost += GameLost;

        turretBuilder.TurretPlaced += TakeCoins;
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
        Debug.Log("Player Lost");
        PlayerWon?.Invoke(false);
    }

    void GameWon()
    {
        Debug.Log("Player Won!");
        PlayerWon?.Invoke(true);
    }

    void EarnCoins(float reward)
    {
        playerManager.EarnCoins(reward);
    }

    void TakeCoins(int amount)
    {
        playerManager.EarnCoins(-amount);
    }
    
}
