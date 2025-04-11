using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour, IObjectPoolable
{
    public int damage = 10; // Damage dealt by the projectile
    public ForceType forceType; // Type of force applied to the projectile
    [SerializeField] GameObject explosionHitParticle;
    [SerializeField] Rigidbody rb; // Rigidbody component of the projectile

    [Header("Object Pooling")]

    [SerializeField] int poolSize = 10; // Size of the object pool
    public int PoolSize { get => poolSize; }
    [SerializeField] string poolTag; // Tag for the object pool
    public string Tag { get => poolTag; }

    public bool IsInUse { get ; set ; }

    public void Release()
    {
        rb.velocity = Vector3.zero; // Reset velocity
        IsInUse = false;
    }

    public void Use()
    {
        IsInUse = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {
            ObjectPoolerManager.Instance.InstantiatePoolObject(explosionHitParticle, transform.position, Quaternion.identity);
            ObjectPoolerManager.Instance.ReleasePoolObject(gameObject);
        }
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, forceType);
            ObjectPoolerManager.Instance.ReleasePoolObject(gameObject);
        }

    }
}

