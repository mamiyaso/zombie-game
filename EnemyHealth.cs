using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; 
    public float currentHealth; 
    public Animator animator;
    public static int totalZombiesKilled = 0;
    public static float totalDamageDealt = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        totalDamageDealt += damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        GetComponent<ZombieAI>().enabled = false; 
        GetComponent<NavMeshAgent>().enabled = false; 
        GetComponent<Collider>().enabled = false; 

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        ZombieSpawner.ZombieDied(); 
        totalZombiesKilled++;
        Destroy(gameObject);
    }
}