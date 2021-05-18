using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{
    [SerializeField] private PoolItem _prefab = default;
    [SerializeField, Range(0, 100)] private int _defaultSize = 0;

    private List<PoolItem> _actives = new List<PoolItem>();
    private List<PoolItem> _inactives = new List<PoolItem>();


    private void Start()
    {
        for(int i = 0; i < _defaultSize; i++)
        {
            AddToPool();
        }
    }

    private void AddToPool()
    {
        PoolItem obj = Instantiate(_prefab, transform);
        obj.OnRemove(OnRemoveCallback);
    }

    public PoolItem GetAPoolObject()
    {
        int index = _inactives.Count - 1;
        if(index < 0)
        {
            AddToPool();
            index = 0;
        }

        PoolItem obj = _inactives[index];
        _inactives.RemoveAt(index);
        _actives.Add(obj);
        obj.Activate();
        return obj;
    }


    public void OnRemoveCallback(PoolItem obj)
    {
        _actives.Remove(obj);
        _inactives.Add(obj);
    }
}
