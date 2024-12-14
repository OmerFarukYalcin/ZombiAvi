using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TaskManager
{
    private Dictionary<string, Task> taskDict; // Dictionary for fast task lookup

    public TaskManager(List<GameObject> _tasks)
    {
        // Populate the dictionary with tasks using their names as keys
        taskDict = _tasks.ToDictionary(go => go.name, go => new Task(go.transform));
    }

    public void CollectTask(Transform tf)
    {
        // Mark a task as collected if it exists in the dictionary
        if (taskDict.TryGetValue(tf.name, out Task task))
        {
            task.SetCollected(true);
        }
    }

    public void PutAwayTask(Transform tf)
    {
        // Mark a task as not collected if it exists in the dictionary
        if (taskDict.TryGetValue(tf.name, out Task task))
        {
            task.SetCollected(false);
        }
    }

    public List<Task> GetCollectedTasks()
    {
        // Retrieve all tasks that are currently collected
        return taskDict.Values.Where(s => s.collected).ToList();
    }
}

[System.Serializable]
public class Task
{
    [SerializeField] private Transform transformReference; // Reference to the task's Transform
    public bool collected { get; private set; } // Indicates whether the task is collected

    public Task(Transform tf)
    {
        transformReference = tf;
    }

    public Transform transformRef => transformReference;

    public void SetCollected(bool val) => collected = val;
}
