using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents the Obelisk object that manages tasks and triggers game completion.
/// </summary>
public class Obelisk : MonoBehaviour
{
    // Event triggered when the game ends
    public event Action onGameEnded;

    [SerializeField] private List<GameObject> tasks = new List<GameObject>(); // List of task objects associated with the obelisk
    [SerializeField] private Material taskMaterial; // Material to apply to completed tasks
    private int completedTaskCount = 0; // Tracks the number of completed tasks
    public bool gameComplete; // Indicates if the game is completed

    private void OnEnable()
    {
        // Subscribe to the onGameEnded event
        onGameEnded += HandleOnGameEnded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the onGameEnded event
        onGameEnded -= HandleOnGameEnded;
    }

    /// <summary>
    /// Handles the actions to perform when the game ends.
    /// </summary>
    private void HandleOnGameEnded()
    {
        gameComplete = true;

        // Hide the obelisk after a delay
        Invoke(nameof(HideObelisk), 1f);
    }

    /// <summary>
    /// Places collected task objects into the obelisk.
    /// </summary>
    public void PutTaskObject()
    {
        // Retrieve all collected tasks from the TaskManager
        var collectedTasks = GameControl.instance.TaskManager.GetCollectedTasks();

        foreach (var task in collectedTasks)
        {
            // Find the corresponding task object in the obelisk's task list
            GameObject obeliskTask = tasks.FirstOrDefault(obj => obj.name == task.transformRef.name);

            if (obeliskTask == null)
            {
                Debug.LogWarning($"Task '{task.transformRef.name}' does not match any obelisk task.");
                return;
            }

            // Mark the task as no longer collected in the TaskManager
            GameControl.instance.TaskManager.PutAwayTask(task.transformRef);

            // Update the task object's material to indicate completion
            obeliskTask.GetComponent<Renderer>().material = taskMaterial;

            // Increment the completed task counter
            completedTaskCount++;

            // Check if all tasks are completed
            if (completedTaskCount == tasks.Count)
            {
                // Trigger the game end event
                onGameEnded?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// Hides the obelisk after the game ends.
    /// </summary>
    private void HideObelisk()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
