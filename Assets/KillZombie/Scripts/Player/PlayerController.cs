using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioClip[] sounds; 
    
    public GameControl gControl;
    public Image healtImage;
    
    private float healtAmount = 5f;
    
    private AudioSource aSourse;

    void Start()
    {
        aSourse = GetComponent<AudioSource>();
    }

    public void playShootSound()
    {
        aSourse.PlayOneShot(sounds[0], 1f);
    }

    public void playHurtSound()
    {
        aSourse.PlayOneShot(sounds[3], 1f);
    }

    public void playTakingHeartSound()
    {
        aSourse.PlayOneShot(sounds[2], 1f);
    }

    public void playDieSound()
    {
        aSourse.PlayOneShot(sounds[1], 1f);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag.Equals("zombi"))
        {
            GetComponent<PlayerHealt>().TakeDamage(healtAmount, healtImage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("kalp"))
        {
            GetComponent<PlayerHealt>().IncreaseHealt(healtAmount, healtImage, other.gameObject);
        }
        if(other.gameObject.name.Equals("WaterProDaytime"))
        {
            GetComponent<PlayerHealt>().TakeDamage(100, healtImage);
        }
        if (other.gameObject.tag.Equals("Finish"))
        {
            gControl.GameComplete();
        }
    }
}
