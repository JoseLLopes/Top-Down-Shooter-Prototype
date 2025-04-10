using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnTrigger : MonoBehaviour, IObjectPoolable
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] ForceType forceType = ForceType.Light;

    [Header("Object Pooling")]
    [SerializeField] private int poolSize = 10; // Size of the object pool
    public int PoolSize => poolSize;

    [SerializeField] private string tag; // Tag for the object pool
    public string Tag => tag;

    public bool IsInUse { get; set; }

    Coroutine releaseCoroutine;

    public void Release()
    {
        // Reset any necessary properties here
        IsInUse = false;
        if (releaseCoroutine != null)
        {
            StopCoroutine(releaseCoroutine);
            releaseCoroutine = null;
        }
    }

    public void Use()
    {
        IsInUse = true;
    }

    private void Start()
    {
        releaseCoroutine = StartCoroutine(TimeToRelease());
    }

    IEnumerator TimeToRelease()
    {
        yield return new WaitForSeconds(0.4f);
        ObjectPoolerManager.Instance.ReleasePoolObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Health component
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            // Deal damage to the object
            damageable.TakeDamage(damageAmount);
            ObjectPoolerManager.Instance.ReleasePoolObject(gameObject);
        }
    }
}
