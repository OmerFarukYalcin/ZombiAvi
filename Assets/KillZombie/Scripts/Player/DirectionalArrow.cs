using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private List<GameObject> lastTargets = new();
    private List<GameObject> targets = new();
    [Range(1, 5)]
    [SerializeField] private float rotationSpeed;
    private int counter = 0;
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("task").ToList();

        foreach (GameObject go in lastTargets)
        {
            targets.Add(go);
        }
    }

    void Update()
    {
        if (targets[counter].gameObject.activeInHierarchy == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targets[counter].transform.position - transform.position),
                rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (counter < 6)
                counter += 1;
        }
    }
}
