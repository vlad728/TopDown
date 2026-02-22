using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Animator animator;
    public void KillEnemy()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool("death", true);
        
    }
}
