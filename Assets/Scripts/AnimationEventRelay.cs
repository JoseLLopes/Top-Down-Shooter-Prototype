using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] Ai_Movement aiMovement; // Referência ao dono do ataque

    public void OnAttackHit()
    {
        if (aiMovement != null)
        {
            aiMovement.OnAttackImpact(); // delega a lógica de ataque
        }
    }
}