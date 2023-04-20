using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public Action onInit;
    public Action onReturn;
 
    private bool _isInit;
 
    public void Init()
    {
        if (_isInit) return;
        foreach (var component in GetComponentsInChildren<MonoBehaviour>())
        {
            if(component is IPoolInitAble initAble)onInit += initAble.Init;
            if(component is IPoolReturnAble returnAble)onReturn += returnAble.Return;
        }
 
        _isInit = true;
    }
 
    private void Awake()
    {
        Init();
    }
}
