using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;

    public void TakeDamage(int damage, ForceType forceType = ForceType.Light)
    {
        currentHealth -= damage;

    }
}
