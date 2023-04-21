using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Level/New Level Data", order = 0, fileName = "New Level Data")]
public class LevelData : ScriptableObject
{
   [SerializeField] private WaveData[] waveData;
   [SerializeField] private NexusData nexusData;

   public WaveData[] WaveData => waveData;
   public NexusData NexusData => nexusData;
}
