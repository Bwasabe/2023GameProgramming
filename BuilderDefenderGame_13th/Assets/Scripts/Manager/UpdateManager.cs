using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoSingleton<UpdateManager>
{
    private List<IUpdateAble> _updateAbleList = new();

    private void Update()
    {
        foreach (IUpdateAble updateAble in _updateAbleList)
        {
            updateAble.OnUpdate();
        }
    }

    public void RegisterObject(IUpdateAble updateAble)
    {
        if(!_updateAbleList.Contains(updateAble))
            _updateAbleList.Add(updateAble);
        else
            Debug.Log("Updateable Object is already Registered");
    }

    public void UnRegisterObject(IUpdateAble updateAble)
    {
        _updateAbleList.Remove(updateAble);
    }
}
