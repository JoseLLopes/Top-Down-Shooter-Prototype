using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager Instance;
    [SerializeField] List<GameObject> poolableObjects;
    List<ObjectPool> objectPools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        objectPools = new List<ObjectPool>();
        foreach (var obj in poolableObjects)
        {
            if (obj.GetComponent<IObjectPoolable>() == null)
            {
                Debug.LogError("Object does not implement IObjectPoolable interface");
                continue;
            }
            GameObject poolGameObjectParent = new GameObject(obj.GetComponent<IObjectPoolable>().Tag);
            poolGameObjectParent.transform.SetParent(transform, false);
            ObjectPool pool = poolGameObjectParent.AddComponent<ObjectPool>();
            pool.tag = obj.GetComponent<IObjectPoolable>().Tag;

            for (int i = 0; i < 10; i++)
            {
                var instantiatedObject = Instantiate(obj, poolGameObjectParent.transform);
                IObjectPoolable poolable = instantiatedObject.GetComponent<IObjectPoolable>();
                poolable.IsInUse = false;
                instantiatedObject.SetActive(false);
                pool.poolObjects.Add(instantiatedObject);
            }
            objectPools.Add(pool);
        }
    }

    public GameObject InstantiatePoolObject(GameObject obj, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        Debug.Log("Instantiating object: " + obj.name);
        if (obj.TryGetComponent<IObjectPoolable>(out var poolable))
        {
            Debug.Log("Object implements IObjectPoolable interface");
            return InstantiatePoolObject(poolable.Tag, position, rotation, parent);
        }
        else
        {
            Debug.LogError("Object does not implement IObjectPoolable interface");
            return null;
        }
    }

    public GameObject InstantiatePoolObject(string tag, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        ObjectPool myPool = objectPools.Find(x => x.tag == tag);
        if (myPool != null)
        {
            var poolObject = myPool.GetPoolObject();
            if(poolObject == null)
            {
                GameObject objectPrefab = poolableObjects.Find(x => x.GetComponent<IObjectPoolable>().Tag == tag);
                poolObject = Instantiate(objectPrefab, myPool.transform);
            }
            Debug.Log("Instantiated object: " + poolObject.name);
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            poolObject.transform.SetParent(parent);
            poolObject.SetActive(true);
            return poolObject;
        }
        return null;
    }

    internal void ReleasePoolObject(GameObject objectToRelease)
    {
        if (objectToRelease.TryGetComponent<IObjectPoolable>(out var poolable))
        {
            ObjectPool myPool = objectPools.Find(x => x.tag == poolable.Tag);
            if (myPool != null)
            {
                myPool.ReleasePoolObject(objectToRelease);
            }
            else
            {
                Debug.LogError("No pool found for the object with tag: " + poolable.Tag);
            }
        }
        else
        {
            Debug.LogError("Object does not implement IObjectPoolable interface");
        }
    }
}
