using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Level/New Level Data", order = 0, fileName = "New Level Data")]
public class LevelData : ScriptableObject
{
   [SerializeField] private WaveData[] waveData;
   [SerializeField] private NexusData nexusData;
   [SerializeField] private int startingCoins;

   public WaveData[] WaveData => waveData;
   public NexusData NexusData => nexusData;

   public int StartingCoins => startingCoins;
}
