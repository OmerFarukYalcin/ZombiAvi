using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(new Vector3(250,0,250),Vector3.right,1f*Time.deltaTime);
    }
}
