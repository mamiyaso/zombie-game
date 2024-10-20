using UnityEngine;

public class BreakableWoodScript : MonoBehaviour
{
    public float maxHealth = 1f; 
    private float currentHealth; 

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}