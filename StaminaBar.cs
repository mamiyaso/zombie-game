using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider staminaSlider2;
    private float lerpSpeed = 0.05f;

    private Stamina playerStamina;

    void Start()
    {
        playerStamina = FindObjectOfType<Stamina>();
        staminaSlider.maxValue = playerStamina.maxStamina;
        staminaSlider2.maxValue = playerStamina.maxStamina;
    }

    void Update()
    {
        if (staminaSlider.value != playerStamina.currentStamina)
        {
            staminaSlider.value = playerStamina.currentStamina;
        }

        if (staminaSlider2.value != playerStamina.currentStamina)
        {
            staminaSlider2.value = Mathf.Lerp(staminaSlider2.value, playerStamina.currentStamina, lerpSpeed);
        }
    }
}
