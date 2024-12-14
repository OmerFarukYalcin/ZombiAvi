using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image healtImage; // UI image to display health
    private PlayerHealt playerHealt; // Cached reference to PlayerHealt component

    [SerializeField] private float healtAmount = 5f; // Health change amount
    [SerializeField] private float waterDamageAmount = 100f; // Damage amount for water hazard

    private void Start()
    {
        // Cache the PlayerHealt component
        playerHealt = GetComponent<PlayerHealt>();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.CompareTag("zombie"))
        {
            // Reduce health when colliding with a zombie
            playerHealt.TakeDamage(healtAmount, healtImage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hearth"))
        {
            // Increase health when collecting a health item
            playerHealt.IncreaseHealt(healtAmount, healtImage, other.gameObject);
        }
        else if (other.gameObject.name.Equals("WaterProDaytime"))
        {
            // Apply water hazard damage
            playerHealt.TakeDamage(waterDamageAmount, healtImage);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            // Trigger game completion
            GameControl.instance.GameComplete();
        }
    }
}
