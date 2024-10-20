using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void PlayRunAnimation()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", false);
    }

    public void PlayAttackAnimation()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }
}