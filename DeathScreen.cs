using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI damageTakenText;
    public TextMeshProUGUI damageDealtText;
    public Button mainMenuButton; 

    void Start()
    {
        deathScreenUI.SetActive(false);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void DisplayDeathScreen()
    {
        deathScreenUI.SetActive(true);
        killsText.text = "Zombies Killed: " + EnemyHealth.totalZombiesKilled;
        damageTakenText.text = "Damage Taken: " + PlayerHealth.totalDamageTaken;
        damageDealtText.text = "Damage Dealt: " + EnemyHealth.totalDamageDealt;
        Time.timeScale = 0f; 

        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None; 
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None; 
        SceneManager.LoadScene("Main Menu"); 
    }

}