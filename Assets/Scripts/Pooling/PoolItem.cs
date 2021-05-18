
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolItem : MonoBehaviour
{
    public Action<PoolItem> _onRemoveCallback;

    public void OnRemove(Action<PoolItem> callback)
    {
        _onRemoveCallback = callback;
        Remove();
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Remove()
    {
        Debug.Log(_onRemoveCallback);
        _onRemoveCallback?.Invoke(this);
        gameObject.SetActive(false);
    }    
}
