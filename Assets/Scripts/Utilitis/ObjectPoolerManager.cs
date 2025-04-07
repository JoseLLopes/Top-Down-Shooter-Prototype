using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
}
