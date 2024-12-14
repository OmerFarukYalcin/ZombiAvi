using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    private const float MaxHealth = 100f; // Maximum player health
    private float health = MaxHealth;    // Player's current health

    /// <summary>
    /// Reduces the player's health by a specified amount and updates the UI.
    /// </summary>
    public void TakeDamage(float amount, Image healthImage)
    {
        BGMUSIC.instance.PlaySfx("hit");

        health -= amount;
        health = Mathf.Clamp(health, 0, MaxHealth); // Ensure health doesn't go below 0
        UpdateHealthBar(healthImage);

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Increases the player's health by a specified amount and updates the UI.
    /// </summary>
    public void IncreaseHealt(float amount, Image healthImage, GameObject pickup)
    {
        BGMUSIC.instance.PlaySfx("takeHealt");

        health = Mathf.Min(health + amount, MaxHealth); // Increase health without exceeding MaxHealth
        UpdateHealthBar(healthImage);

        Destroy(pickup); // Destroy the health pickup object
    }

    /// <summary>
    /// Updates the health bar's fill amount and color based on the current health.
    /// </summary>
    private void UpdateHealthBar(Image healthImage)
    {
        float healthRatio = health / MaxHealth;
        healthImage.fillAmount = healthRatio;
        healthImage.color = Color.Lerp(Color.red, Color.green, healthRatio);
    }

    /// <summary>
    /// Handles the player's death logic.
    /// </summary>
    private void Die()
    {
        BGMUSIC.instance.PlaySfx("death");
        GameControl.instance.GameOver();
    }
}
