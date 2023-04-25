using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Scriptable Data/New Scriptable Data", order = 0, fileName = "New Scriptable Data")]
public class ScriptableData : ScriptableObject
{
    [SerializeField] private string dataName;
    [SerializeField] private int id;
    [SerializeField] private GameObject prefab;

    
    public int Id => id;
    public GameObject Prefab => prefab;

    public string DataName => dataName;
}
