using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OyunKontrol : MonoBehaviour
{
    public GameObject zombi;
    public Text puanText;
    private int puan;
    private float zamanSayaci;
    private float olusumSureci=10f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        zamanSayaci = olusumSureci;
    }

    // Update is called once per frame
    void Update()
    {
        zamanSayaci -= Time.deltaTime;
        if(zamanSayaci < 0)
        {
            Vector3 pos = new Vector3(Random.Range(204f, 343f), 22f, Random.Range(169f, 293f));
            Instantiate(zombi, pos, Quaternion.identity);
            zamanSayaci = olusumSureci;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void PuanArttir(int p)
    {
        puan += p;
        puanText.text = "" + puan;
    }

    public void OyunBitti()
    {
        PlayerPrefs.SetInt("puan", puan);
        SceneManager.LoadScene("OyunBitti");
    }
}
