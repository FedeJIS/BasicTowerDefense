using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private WavesManager wavesManager;
    [SerializeField] private PlayerManager playerManager;

    private bool playerLost;
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
        if (playerLost) return;
        playerLost = true;
        Debug.Log("Player Lost");
    }

    void GameWon()
    {
        Debug.Log("Player Won!");
    }
    
}
