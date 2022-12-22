using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuKontrol : MonoBehaviour
{
    public AudioClip atisSesi, olmeSesi, canAlmaSesi, yaralanmaSesi;
    public Transform mermiPos;
    public GameObject mermi;
    public GameObject Patlama;
    public OyunKontrol oyunKonrolScripti;
    public Image canImaji;
    private float canDegeri = 100f;
    private AudioSource aSourse;
    // Start is called before the first frame update
    void Start()
    {
        aSourse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            aSourse.PlayOneShot(atisSesi, 1f);
            GameObject go = Instantiate(mermi, mermiPos.position,mermiPos.rotation);
            GameObject goPatlama = Instantiate(Patlama, mermiPos.position, mermiPos.rotation);
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 10f;
            Destroy(go.gameObject, 2f);
            Destroy(goPatlama.gameObject, 2f);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag.Equals("zombi"))
        {
            aSourse.PlayOneShot(yaralanmaSesi, 1f);
            canDegeri -= 5f;
            float x = canDegeri / 100f;
            canImaji.fillAmount = x;
            canImaji.color = Color.Lerp(Color.red, Color.green, x);

            if(canDegeri <= 0)
            {
                aSourse.PlayOneShot(olmeSesi, 1f);
               oyunKonrolScripti.OyunBitti();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("kalp"))
        {
            aSourse.PlayOneShot(canAlmaSesi, 1f);
            if (canDegeri < 100f)
                canDegeri += 5f;
                float x = canDegeri / 100f;
                canImaji.fillAmount = x;
                canImaji.color = Color.Lerp(Color.red, Color.green, x);
                Destroy(other.gameObject);
            
        }
    }
}
