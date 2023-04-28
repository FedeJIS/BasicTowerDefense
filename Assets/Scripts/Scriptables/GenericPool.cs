using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenericPool<T> where T : ScriptableData
{
    private List<Tuple<T, GameObject>> _pool;
    private GenericFactory<T> _factory;

    public GenericPool(string dataPath)
    {
        _pool = new List<Tuple<T, GameObject>>();
        _factory = new GenericFactory<T>(dataPath);
    }

    public Tuple<T, GameObject> Get(int id)
    {
        Tuple<T, GameObject> obj = null;

        if (_pool.Count > 0)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].Item1.Id != id || _pool[i].Item2.activeSelf) continue;
                obj = _pool[i];
                break;
            }
        }
        
        if(obj == null)
        {
            obj = _factory.FabricateById(id);
            _pool.Add(obj);
        }

        obj?.Item2.gameObject.SetActive(true);

        return obj;
    }

    public Tuple<T, GameObject> GetRandom()
    {
        //Creates a totally new Random element from the list of elements to be created
        var elementsList = _factory.GetAllData();
        var newRandom = Random.Range(0, elementsList.Count);
        var newElement = elementsList[newRandom];

        return Get(newElement.Id);


    }
}
