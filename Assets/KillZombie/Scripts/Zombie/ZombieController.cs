using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float damage = 1f; // Damage dealt by the zombie
    [SerializeField] private float detectionRange = 10f; // Range at which the zombie detects the player

    private Target target; // Reference to the zombie's Target component
    private GameObject player; // Reference to the player object
    private float distance; // Current distance between the zombie and the player
    private AudioSource audioSource; // AudioSource component for zombie sounds
    private NavMeshAgent navMeshAgent; // NavMeshAgent component for movement

    void Start()
    {
        // Initialize references to required components
        target = GetComponent<Target>();
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Find the player GameObject by name
        player = GameObject.Find("FPSController");
    }

    void Update()
    {
        if (target == null || navMeshAgent == null || player == null) return;

        // Update the zombie's navigation and behavior based on its state
        if (!target.IsDeath())
        {
            // If the zombie is alive, move towards the player
            navMeshAgent.destination = player.transform.position;
        }
        else
        {
            // If the zombie is dead, stop moving and freeze rotation
            navMeshAgent.destination = transform.position;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        }

        // Calculate the distance between the zombie and the player
        distance = Vector3.Distance(transform.position, player.transform.position);

        // Play attack animation and sound if within range
        HandleZombieBehavior();
    }

    private void HandleZombieBehavior()
    {
        if (distance < detectionRange)
        {
            // Play zombie sound if not already playing
            if (!audioSource.isPlaying)
                audioSource.Play();

            // Play attack animation if zombie is alive
            if (!target.IsDeath())
                GetComponentInChildren<Animation>().Play("Zombie_Attack_01");
        }
        else
        {
            // Stop zombie sound if out of range
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with bullets
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle trigger collisions with bullets
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
