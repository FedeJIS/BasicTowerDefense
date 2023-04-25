using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Waves/New Wave Data", order = 0, fileName = "New Wave Data")]
public class WaveData : ScriptableObject
{
   [SerializeField] private string waveName;
   [SerializeField] private int creepsAmount;
   [SerializeField] private float timeToSpawn;
   
   public int CreepsAmount => creepsAmount;
   public float TimeToSpawn => timeToSpawn;

   public string WaveName => waveName;

}
