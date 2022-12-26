using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionalArrow : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    public float rotationSpeed;
    public int counter = 0;
    void Start()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("task"))
        {
            targets.Add(item.transform);
        }
        targets.Add(GameObject.Find("obelisk").transform);
        targets.Add(GameObject.Find("Finish").transform);
    }

    void Update()
    {
        if (targets[counter].gameObject.activeInHierarchy == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targets[counter].position - transform.position),
                rotationSpeed * Time.deltaTime);
        }
        else
        {
            if(counter<6)
            counter += 1;
        }
    }
}
