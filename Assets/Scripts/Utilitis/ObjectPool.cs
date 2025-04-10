
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
    public string tag;
    public List<GameObject> poolObjects = new List<GameObject>();

    public GameObject GetPoolObject()
    {
        foreach (var poolObject in poolObjects)
        {
            IObjectPoolable poolable = poolObject.GetComponent<IObjectPoolable>();

            if (!poolable.IsInUse)
            {
                poolable.Use();
                return poolObject;
            }
        }

        return null;
    }

    internal void ReleasePoolObject(GameObject objectToRelease)
    {
        IObjectPoolable poolable = objectToRelease.GetComponent<IObjectPoolable>();
        if (poolable != null)
        {
            poolable.Release();
            objectToRelease.SetActive(false);
            objectToRelease.transform.position = Vector3.zero;
            objectToRelease.transform.rotation = Quaternion.identity;
            objectToRelease.transform.SetParent(transform);
        }
        else
        {
            Debug.LogError("Object does not implement IObjectPoolable interface");
        }
    }
}
