using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableParticle : MonoBehaviour, IObjectPoolable
{
    public float lifeTime = 2f;

    [Header("Object Pooling")]
    [SerializeField] int poolSize = 10;
    public int PoolSize => poolSize;
    [SerializeField]
    string poolTag;
    public string Tag => poolTag;

    public bool IsInUse { get; set; }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime(lifeTime));
    }

    private IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPoolerManager.Instance.ReleasePoolObject(gameObject);
    }

    public void Use()
    {
        IsInUse = true;
    }

    public void Release()
    {
        IsInUse = false;
    }
}
