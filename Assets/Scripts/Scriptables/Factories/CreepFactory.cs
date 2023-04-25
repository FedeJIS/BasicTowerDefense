using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Factory", order = 0, fileName = "New Creep Factory")]
public class CreepFactory : ScriptableObject
{
    [SerializeField] private GameObject baseCreep;
    [SerializeField] private GameObject frostCreep;
    

    private float threshold = 0.65f;
    
    private const float DistanceFactor = 5f;
    
    public GameObject FabricateRandomCreep(Transform position)
    {
        var odds = Random.Range(0f, 1f);

        if (odds >= threshold) Instantiate(baseCreep,position);

        return Instantiate(frostCreep,position);
    }

    public GameObject FabricateBaseCreep(Transform position)
    {
        return Instantiate(baseCreep,position);
    }
    
    public GameObject FabricateFrostCreep(Transform position)
    {
        return Instantiate(frostCreep,position);
    }
}
