using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Waves/New Wave Data", order = 0, fileName = "New Wave Data")]
public class WaveData : ScriptableObject
{
   [SerializeField] private int creepsAmount;
   [SerializeField] private int timeToSpawn;

   public int CreepsAmount => creepsAmount;
   public int TimeToSpawn => timeToSpawn;
}
