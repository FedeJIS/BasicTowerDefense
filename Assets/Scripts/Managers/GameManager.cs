using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private WavesManager wavesManager;
    [SerializeField] private PlayerManager playerManager;

    public static Action<bool> PlayerWon;
    void Start()
    {
        wavesManager.WavesData = levelData.WaveData;
        wavesManager.WaveFinished += NextWave;
        wavesManager.NoWavesLeft += GameWon;
        
        playerManager.SetUpPlayer(levelData.NexusData);
        playerManager.PlayerLost += GameLost;

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
    
}
