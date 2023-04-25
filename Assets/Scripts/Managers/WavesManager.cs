using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CreepFactory creepFactory;
    [SerializeField] private Canvas healthCanvas;
    public WaveData[] WavesData { get; set; }

    private int _currentWaveIndex;
    private int _currentCreepsKilled;
    private WaveData _currentWaveData;

    public Action NoWavesLeft;
    public Action WaveFinished;
    private void SpawnWave()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        _currentCreepsKilled = _currentWaveData.CreepsAmount;
        int creepsAmount = _currentWaveData.CreepsAmount;
        var creepsList = new List<BaseCreep>();
        
        while (creepsAmount > 0)
        {
            var randomAmount = GetRandomCreepAmount();
            
            creepsAmount -= randomAmount;

            var creeps = creepFactory.FabricateRandomCreepWave(randomAmount,GetRandomSpawnPoint(),healthCanvas);

            foreach (var creep in creeps)
            {
                creep.CreepKilled += CountKilledCreeps;
            }

            yield return new WaitForSeconds(_currentWaveData.TimeToSpawn);
        }
        
        WaveFinished?.Invoke();
    }

    private void CountKilledCreeps()
    {
        _currentCreepsKilled--;
        
        if(_currentCreepsKilled <= 0) WaveFinished?.Invoke();
    }
    
    public void NextWave()
    {
        if (_currentWaveIndex >= WavesData.Length)
        {
            NoWavesLeft?.Invoke();
            return;
        }
        
        _currentWaveData = WavesData[_currentWaveIndex];
        
        SpawnWave();
        
        _currentWaveIndex++;
    }

    #region Random Methods

    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private int GetRandomCreepAmount()
    {
        return Random.Range(0, _currentWaveData.CreepsAmount);
    }

   

    #endregion

    
    
}
