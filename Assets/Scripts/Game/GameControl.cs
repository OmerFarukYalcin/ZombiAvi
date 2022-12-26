using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{
    public GameObject zombie;
    public Text pointText;
    private int point;
    private float timer;
    private float createTime = 10f;

    [SerializeField] Obelisk obelisk;
    [SerializeField] GameObject finish;
    void Start()
    {
        Cursor.visible = false;
        timer = createTime;
    }

    void Update()
    {
        if (obelisk.gameComplete && !finish.activeInHierarchy)
        {
            finish.SetActive(true);
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Vector3 pos = new Vector3(Random.Range(204f, 343f), 22f, Random.Range(169f, 293f));
            Instantiate(zombie, pos, Quaternion.identity);
            timer = createTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void IncreasePoint(int p)
    {
        point += p;
        pointText.text = "" + point;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("puan", point);
        SceneManager.LoadScene("OyunBitti");
    }
    public void GameComplete()
    {
        PlayerPrefs.SetInt("puan", point);
        SceneManager.LoadScene("OyunuKazandý");
    }
}
