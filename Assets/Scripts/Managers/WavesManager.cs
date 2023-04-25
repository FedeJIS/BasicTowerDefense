using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CreepFactory creepFactory;
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

        while (creepsAmount > 0)
        {
            creepsAmount--;

            var creep = creepFactory.FabricateRandomCreep(GetRandomSpawnPoint()).GetComponent<BaseCreep>();
            
            creep.CreepKilled += CountKilledCreeps;

            yield return new WaitForSeconds(_currentWaveData.TimeToSpawn);
        }

        yield return null;

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


    #endregion

    
    
}
