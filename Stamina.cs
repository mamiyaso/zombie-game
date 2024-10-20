using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 3f;
    public float currentStamina;
    public float staminaRegenRate = 0.4f;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        RegenerateStamina();
    }

    public void UseStamina()
    {
        if (currentStamina >= 1f)
        {
            currentStamina -= 1f;
        }
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime; 
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }
}
