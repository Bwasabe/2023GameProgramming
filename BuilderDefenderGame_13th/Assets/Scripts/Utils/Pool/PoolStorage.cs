using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolStorage : MonoBehaviour
{
    private static readonly Dictionary<GameObject, Queue<GameObject>> PooledObjects = new();
    private static readonly Dictionary<GameObject, List<GameObject>> RegisteredObjects = new();
    private static readonly Dictionary<GameObject, PoolObject> PoolObjects = new();
    
    private static Transform _defaultParent;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        GameObject poolManager = new GameObject("PoolManager");
        poolManager.AddComponent<PoolStorage>();
        //poolManager.hideFlags = HideFlags.HideInHierarchy;

        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(poolManager);
    }

    private static void OnSceneUnloaded(Scene _)
    {
        GameObject[] keys = new GameObject[PooledObjects.Keys.Count];
        PooledObjects.Keys.CopyTo(keys, 0);
        
        foreach (GameObject key in keys)
            if (key == null)
                PooledObjects.Remove(key);

        var registeredKeys = new GameObject[RegisteredObjects.Keys.Count];
        RegisteredObjects.Keys.CopyTo(registeredKeys, 0);
        
        foreach (GameObject key in registeredKeys)
            if (key == null)
                RegisteredObjects.Remove(key);
        
        _defaultParent = null;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _defaultParent = new GameObject("PoolObjects").transform;
    }

    public static GameObject InstantiateObject(GameObject prefab, Transform parent = null)
    {
        parent ??= _defaultParent;
        
        GameObject InitGameObject()
        {
            GameObject instance = Instantiate(prefab, parent);
            RegisterObject(instance, prefab);
            
            if (instance.TryGetComponent(out PoolObject _)) return instance;

            PoolObject poolObject = instance.AddComponent<PoolObject>();
            poolObject.prefab = prefab;
            poolObject.Init();
            PoolObjects.TryAdd(instance, poolObject);
            
            return instance;
        }

        return InitGameObject();
    }

    private static void RegisterObject(GameObject instance, GameObject prefab)
    {
        RegisteredObjects.TryAdd(prefab, new List<GameObject>());

        instance.name = $"{prefab.name} [{RegisteredObjects[prefab].Count}]";
        RegisteredObjects[prefab].Add(instance);
    }

    public static GameObject GetObject(GameObject prefab, Transform parent = null)
    {
        parent ??= _defaultParent;

        PooledObjects.TryAdd(prefab, new Queue<GameObject>());
        
        
        GameObject instance = PooledObjects[prefab].Count > 0 ? PooledObjects[prefab].Dequeue() : InstantiateObject(prefab, parent);
        
        instance.transform.SetParent(parent);
        instance.SetActive(true);
        
        PoolObject poolObject = PoolObjects[instance];
        poolObject.onInit?.Invoke();
        //instance.hideFlags = HideFlags.None;
        
        return instance;
    }

    public static void ReturnObject(GameObject instance)
    {
        if (PoolObjects.TryGetValue(instance, out PoolObject poolObject))
        {
            poolObject.onReturn?.Invoke();
        }
        else
        {
            Debug.LogWarning("Prefab not found for instance " + instance.name);
            Destroy(instance);
            return;
        }

        GameObject prefab = poolObject.prefab;
        
        if (prefab is null)
        {
            Debug.LogWarning("Prefab not found for instance " + instance.name);
            Destroy(instance);
            return;
        }
        
        PooledObjects.TryAdd(prefab, new Queue<GameObject>());

        PooledObjects[prefab].Enqueue(instance);
        instance.SetActive(false);
        //instance.hideFlags = HideFlags.HideInHierarchy;
    }
}
