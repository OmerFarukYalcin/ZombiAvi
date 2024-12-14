using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject heart; // Prefab to instantiate upon zombie's death
    [SerializeField] private int zombiePoint = 10; // Points awarded for killing the zombie
    [SerializeField] private float initialHealth = 3f; // Zombie's initial health
    [SerializeField] private float destroyDelay = 1.667f; // Delay before destroying the zombie object

    private float health; // Current health of the zombie
    private bool zombieDeath = false; // Tracks if the zombie is dead

    private void Start()
    {
        // Initialize health
        health = initialHealth;
    }

    /// <summary>
    /// Reduces the zombie's health by a specified damage amount.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        // Trigger death logic if health falls to 0 or below
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Checks if the zombie is dead.
    /// </summary>
    /// <returns>True if the zombie is dead, otherwise false.</returns>
    public bool IsDeath()
    {
        return zombieDeath;
    }

    /// <summary>
    /// Handles the zombie's death sequence.
    /// </summary>
    private void Die()
    {
        // Disable the collider to prevent further interactions
        GetComponent<CapsuleCollider>().enabled = false;

        // Mark the zombie as dead
        zombieDeath = true;

        // Increase the player's score
        GameControl.instance.IncreasePoint(zombiePoint);

        // Instantiate the heart prefab at the zombie's position
        Instantiate(heart, transform.position, Quaternion.identity);

        // Play the zombie death animation, if available
        var animation = GetComponentInChildren<Animation>();
        if (animation != null)
        {
            animation.Play("Zombie_Death_01");
        }

        // Destroy the zombie GameObject after the animation duration
        Destroy(this.gameObject, destroyDelay);
    }
}
