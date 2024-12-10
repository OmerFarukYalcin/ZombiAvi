using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject heart;

    //c# script
    private GameControl gControl;

    //numeric variables
    private int zombiePoint = 10;

    public float healt = 3f;

    //boolean
    private bool zombieDeath = false;
    public void TakeDamage(float _damage)
    {
        gControl = GameObject.Find("_Scripts").GetComponent<GameControl>();
        healt-= _damage;
        if (healt <= 0)
        {
            Die();
        }
    }

    public bool IsDeath() 
    {
        return zombieDeath;
    }

    void Die()
    {
        GetComponent<CapsuleCollider>().enabled= false;
        zombieDeath = true;
        gControl.IncreasePoint(zombiePoint);
        Instantiate(heart, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        GetComponentInChildren<Animation>().Play("Zombie_Death_01");
        Destroy(this.gameObject, 1.667f);
    }
}
