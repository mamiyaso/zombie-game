using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class ZombieAI : MonoBehaviour
{
    public Transform target;
    public float attackDistance = 2f;
    public float attackDamage = 20f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDead = false;
    public AudioClip zombieSound; 
    private AudioSource audioSource;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = zombieSound;
        StartCoroutine(PlayZombieSound());
    }

    void Update()
    {
        if (isDead) return;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackDistance)
            {
                Attack();
            }
            else
            {
                Run(target.position);
            }
        }
        else
        {
            FindPlayerTarget();
        }
    }

    private void FindPlayerTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    private void Run(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);

        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
    }

    private void Attack()
    {
        if (Time.time > nextAttackTime)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
            target.GetComponent<PlayerHealth>().TakeDamage((int)attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private IEnumerator PlayZombieSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if (!isDead)
            {
                PlaySound();
            }
        }
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(zombieSound);
    }
}