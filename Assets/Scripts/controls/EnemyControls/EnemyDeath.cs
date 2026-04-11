using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDie = false;

    public Animator animator;
    public void KillEnemy()
    {
        isDie = true;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool("death", true);
    }
}
