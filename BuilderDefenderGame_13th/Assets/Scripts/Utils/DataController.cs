using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController<TData> where TData : class
{
    private Dictionary<System.Type, TData> _dataDict = new();

    public T GetData<T>() where T : class, TData
    {
        Type type = typeof(T);
        if (_dataDict.TryGetValue(type, out TData data))
        {
            return data as T;
        }
        else
        {
            throw new System.Exception($"{type} is Null in Dict");
        }
    }

    public void AddData(TData data)
    {
        if(!_dataDict.TryAdd(data.GetType(), data))
        {
            throw new System.Exception($"{data.GetType()} is already exist in dictionary");
        }

    }
}
