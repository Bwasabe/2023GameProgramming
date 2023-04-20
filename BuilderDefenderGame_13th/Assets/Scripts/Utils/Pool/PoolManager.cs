using UnityEngine;

public static class PoolManager
{
    public static void PreloadObject(GameObject prefab, int count)
    {
        for (var i = 0; i < count; i++) PoolStorage.InstantiateObject(prefab);
    }

    public static GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        return PoolStorage.GetObject(prefab, parent);
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation,
        Transform parent = null)
    {
        var obj = PoolStorage.GetObject(prefab, parent);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public static void Destroy(GameObject instance)
    {
        PoolStorage.ReturnObject(instance);
    }
}


public interface IPoolInitAble
{
    void Init();
}

public interface IPoolReturnAble
{
    void Return();
}

