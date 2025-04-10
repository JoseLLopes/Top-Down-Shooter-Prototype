using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] ForceType forceType = ForceType.Light;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Health component
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            // Deal damage to the object
            damageable.TakeDamage(damageAmount);
        }
    }
}
