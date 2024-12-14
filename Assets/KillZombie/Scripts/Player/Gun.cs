using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // UI element to display task information
    [SerializeField] private TextMeshProUGUI infoText;

    // Range of the gun
    [Range(50, 150)]
    [SerializeField] private float range = 100f;

    // Camera used for raycasting
    [SerializeField] private Camera fpsCam;

    // Effect prefab for shooting impact
    [SerializeField] private GameObject effect;

    // Bullet prefab
    [SerializeField] private GameObject bullet;

    // Position from where the bullet is instantiated
    [SerializeField] private Transform bulletPos;

    // Stores information about the object hit by the raycast
    private RaycastHit hit;

    void Update()
    {
        // Check for shooting or task-related inputs
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }

        AdjustRaycast();
    }

    private void AdjustRaycast()
    {
        // Cast a ray from the camera's position forward and handle task-related interactions
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 5))
        {
            TaskInfoRaycast(hit);

            if (Input.GetKeyDown(KeyCode.E))
                TakeTask(hit);

            if (Input.GetKeyDown(KeyCode.F))
                PutTask(hit);
        }
    }

    private void TaskInfoRaycast(RaycastHit hit)
    {
        // Determine whether the raycast hit a valid task or obelisk object
        bool isInfoValid = hit.transform.tag == "task" || hit.transform.name == "obelisk";

        // Set the information string based on the object's tag or name
        string infoString = hit.transform.tag == "task"
            ? "Press E to take goldenCross piece"
            : "Press F to put goldenCross piece";

        // Show or hide the task information based on the raycast hit
        infoText.gameObject.SetActive(isInfoValid);

        // Update the text with the relevant information
        infoText.text = infoString;
    }

    private void TakeTask(RaycastHit hit)
    {
        // Handle taking a task object
        if (hit.transform.tag == "task")
        {
            hit.transform.gameObject.SetActive(false);
            GameControl.instance.TaskManager.CollectTask(hit.transform);
        }
    }

    private void PutTask(RaycastHit hit)
    {
        // Handle placing a task object onto the obelisk
        if (hit.transform.name == "obelisk")
        {
            hit.transform.GetComponent<Obelisk>().PutTaskObject();
        }
    }

    void Shoot()
    {
        // Play sound effect for shooting
        BGMUSIC.instance.PlaySfx("bullet");

        // Instantiate the shooting effect at the bullet's position
        GameObject goEffect = Instantiate(effect, bulletPos.position, bulletPos.rotation);
        Destroy(goEffect.gameObject, 1f);

        // Perform a raycast to determine if the bullet hits an object
        bool hitObj = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);

        // Instantiate the bullet and handle the impact
        InstantiateBullet(hit, hitObj);
    }

    void InstantiateBullet(RaycastHit _hit, bool hitting)
    {
        // Handle bullet behavior based on whether it hits an object
        GameObject impactGo = Instantiate(bullet, bulletPos.position, bulletPos.rotation);

        if (hitting)
        {
            // Add force to the bullet towards the hit point
            impactGo.GetComponent<Rigidbody>().AddForceAtPosition(
                (_hit.point - bulletPos.position).normalized * 500f,
                _hit.point.normalized,
                ForceMode.Force
            );
        }
        else
        {
            // Add velocity to the bullet if it doesn't hit any object
            impactGo.GetComponent<Rigidbody>().velocity = bulletPos.transform.forward * 10f;
        }

        // Destroy the bullet after 2 seconds
        Destroy(impactGo, 2f);
    }
}
