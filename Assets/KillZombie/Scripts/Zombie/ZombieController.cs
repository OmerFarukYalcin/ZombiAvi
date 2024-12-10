using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float damage = 1f;

    Target target;
    //Gameobjects
    private GameObject player;
    //public GameObject heart;

    //numeric variables
    //private int zombieHealt = 3;
    //private int zombiePoint = 10;
    private float distance;
    
    //c# script
   // private GameControl gControl;
    
    //Audio
    private AudioSource aSource;
    
    //boolean
   // private bool zombieDeath=false;
    void Start()
    {
        target = GetComponent<Target>();
        aSource = GetComponent<AudioSource>();
        player = GameObject.Find("FPSController");     
    }

    void Update()
    {
        if (!target.IsDeath())
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = gameObject.transform.position;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            print("aaaaa:" + GetComponent<Rigidbody>().constraints);
        }

        distance = Vector3.Distance(transform.position,player.transform.position);
        if(distance < 10f)
        {
            if (!aSource.isPlaying)
                aSource.Play();
            if(!target.IsDeath())
            GetComponentInChildren<Animation>().Play("Zombie_Attack_01");
        }
        else
            if(aSource.isPlaying)
            aSource.Stop();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("mermi"))
        {
            Destroy(c.gameObject);
            GetComponent<Target>().TakeDamage(damage);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("mermi"))
        {
            Destroy(other.gameObject);
            //Debug.Log("Carpisma Gerceklesti");
            //zombieHealt--;
            //if (zombieHealt == 0)
            //{
            //    zombieDeath = true;
            //    gControl.IncreasePoint(zombiePoint);
            //    Instantiate(heart, new Vector3(transform.position.x,transform.position.y+2f,transform.position.z), Quaternion.identity);
            //    GetComponentInChildren<Animation>().Play("Zombie_Death_01");
            //    Destroy(this.gameObject, 1.667f);
            //}
        }
    }
}
