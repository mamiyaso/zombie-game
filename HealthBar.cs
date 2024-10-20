using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider healthSlider2;
    private float lerpSpeed = 0.05f;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider2.maxValue = playerHealth.maxHealth;
    }

    void Update()
    {
        float currentHealth = playerHealth.GetCurrentHealth();

        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider2.value != currentHealth)
        {
            healthSlider2.value = Mathf.Lerp(healthSlider2.value, currentHealth, lerpSpeed);
        }
    }
}
