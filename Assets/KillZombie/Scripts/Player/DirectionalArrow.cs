using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private List<GameObject> lastTargets = new();
    private List<GameObject> targets = new();
    [Range(1, 5)][SerializeField] private float rotationSpeed;
    private int counter = 0;

    void Start()
    {
        // Initialize the targets list with tasks and lastTargets
        targets = GameControl.instance.taskGoList;
        targets.AddRange(lastTargets);
    }

    void Update()
    {
        // Check if the targets list is valid and not empty
        if (targets == null || targets.Count == 0) return;

        // Check if the current target is active in the hierarchy
        if (targets[counter].gameObject.activeInHierarchy)
        {
            // Smoothly rotate the arrow to point towards the active target
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(targets[counter].transform.position - transform.position),
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // Move to the next target if the current one is inactive
            if (counter < targets.Count - 1)
                counter++;
        }
    }
}
