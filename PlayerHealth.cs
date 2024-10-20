using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public static bool isDead = false;
    public static int totalDamageTaken = 0;
    private DeathScreen deathScreen;
    public Gun playerGun; 

    void Start()
    {
        InitializeHealth();
        deathScreen = FindObjectOfType<DeathScreen>();

        if (deathScreen == null)
        {
            Debug.LogError("DeathScreen not found in the scene!");
        }

        if (playerGun == null)
        {
            playerGun = GetComponentInChildren<Gun>();
        }
    }

    void InitializeHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        totalDamageTaken = 0;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        totalDamageTaken += damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        Debug.Log("Player died.");
        isDead = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");

        if (playerGun != null)
        {
            playerGun.enabled = false; 
        }

        ScoreEntry newEntry = new ScoreEntry
        {
            zombiesKilled = EnemyHealth.totalZombiesKilled,
            damageTaken = totalDamageTaken,
            damageDealt = EnemyHealth.totalDamageDealt
        };
        LeaderboardManager.instance.AddNewScore(newEntry);

        gameObject.SetActive(false); 
    }

}