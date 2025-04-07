using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the projectile
    public ForceType forceType; // Type of force applied to the projectile
    [SerializeField] GameObject explosionHitParticle;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {
            Instantiate(explosionHitParticle, transform.position, Quaternion.identity);
        }
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, forceType);
        }

        Destroy(gameObject);
    }
}

