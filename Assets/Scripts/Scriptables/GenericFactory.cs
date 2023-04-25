using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericFactory<T> where T : ScriptableData
{
    private T[] _scriptableData;
    
    private Dictionary<int, T> _map;

    public GenericFactory(string path)
    {
        _scriptableData = Resources.LoadAll<T>(path);
        _map = new Dictionary<int, T>();

        foreach (var data in _scriptableData)
        {
            if (_map.ContainsKey(data.Id)) continue;
            _map.Add(data.Id, data);
        }
    }

    public GameObject FabricateById(int id)
    {
        var scriptableData = GetDataFromId(id);

        if (scriptableData == null) return null;

        var scriptableInstance =  GameObject.Instantiate(scriptableData.Prefab);
        
        scriptableInstance.name = scriptableData.DataName + scriptableData.Id;
        

        return scriptableInstance;
    }

    public GameObject FabricateRandomInPosition(Transform position)
    {
        return GameObject.Instantiate(GetRandomElement(), position);
    }

    private T GetDataFromId(int id)
    {
        if (!_map.ContainsKey(id)) return null;

        return _map[id];
    }
    
    private GameObject GetRandomElement()
    {
        System.Random rand = new System.Random();
        var count = _map.Count;
        var index = rand.Next(0, count);
        var data =  _map.ElementAt(index).Value;
        return data.Prefab;
    }

    public List<T> GetAllData()
    {
        return _map.Values.ToList();
    }
}
