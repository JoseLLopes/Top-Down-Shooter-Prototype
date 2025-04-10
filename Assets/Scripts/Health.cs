using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] Animator animator;

    public void TakeDamage(int damage, ForceType forceType = ForceType.Light)
    {
        currentHealth -= damage;
        animator.SetTrigger("hitLight");
    }
}
