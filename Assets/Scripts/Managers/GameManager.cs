using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private WavesManager wavesManager;
    void Start()
    {
        wavesManager.WavesData = levelData.WaveData;
        wavesManager.NextWave();
    }
    
}
