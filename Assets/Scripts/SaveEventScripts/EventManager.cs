using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Big Company way
    private const string RESSOURCE_NAME = "EventManager";
    private Dictionary<EventID, Action<object>> _eventDict;

    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>(RESSOURCE_NAME);
                Instantiate(prefab);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _eventDict = new Dictionary<EventID, Action<object>>();
    }

    public void RegisterEvent(EventID key, Action<object> callback)
    {
        if(_eventDict.ContainsKey(key))
        {
            _eventDict[key] += callback;
        }
        else
        {
            _eventDict.Add(key, callback);
        }
    }

    public void UnregisterEvent(EventID key, Action<object> callback)
    {
        if(_eventDict.ContainsKey(key))
        {
            _eventDict[key] -= callback;
        }
        else
        {
            Debug.LogWarning("The event you are unregistering does not exist");
        }
    }

    public void DispatchEvent(EventID key, object param = null)
    {
        if(_eventDict.ContainsKey(key))
        {
            _eventDict[key](param);
        }
        else
        {
            Debug.LogWarning("The event you are dispatching does not exist");
        }
    }

}
