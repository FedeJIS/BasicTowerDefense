using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    public WaveData[] WavesData { get; set; }

    private int _currentWaveIndex;
    private int _currentCreepsKilled;
    private WaveData _currentWaveData;

    public Action NoWavesLeft;
    public Action WaveFinished;
    public Action<int> CreepKilled;

    private GenericFactory<CreepData> creepFactory;

    private const string dataPath = "ScriptableData/CreepsData";
    private void Start()
    {
        creepFactory = new GenericFactory<CreepData>(dataPath);
    }

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

            var creep = creepFactory.FabricateRandomInPosition(GetRandomSpawnPoint()).GetComponent<BaseCreep>();
            
            creep.CreepKilled += CountKilledCreeps;

            yield return new WaitForSeconds(_currentWaveData.TimeToSpawn);
        }

        yield return null;

    }

    private void CountKilledCreeps(int reward)
    {
        _currentCreepsKilled--;
        
        CreepKilled?.Invoke(reward);
        
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
