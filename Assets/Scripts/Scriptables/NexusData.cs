using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Level/New Nexus Data", order = 0, fileName = "New Nexus Data")]
public class NexusData : ScriptableObject
{
    [SerializeField] private int nexusHealth;

    public int NexusHealth => nexusHealth;
}
