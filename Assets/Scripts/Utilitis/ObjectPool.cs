
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
}
