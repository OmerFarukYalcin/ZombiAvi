using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameControl : MonoBehaviour
{
    public GameObject zombie;
    public Text pointText;
    private int point;
    private float createTime = 10f;
    [SerializeField] Obelisk obelisk;
    [SerializeField] GameObject finish;

    private void Awake()
    {
        Cursor.visible = false;
    }

    IEnumerator Start()
    {
        while (!obelisk.gameComplete)
        {
            yield return new WaitForSeconds(createTime);
            CreateZombie();
        }
    }

    void Update()
    {
        if (obelisk.gameComplete && !finish.activeInHierarchy)
        {
            finish.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void CreateZombie()
    {
        Vector3 pos = new Vector3(Random.Range(204f, 343f), 22f, Random.Range(169f, 293f));
        Instantiate(zombie, pos, Quaternion.identity);
    }

    public void IncreasePoint(int p)
    {
        point += p;
        pointText.text = "" + point;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("point", point);
        SceneManager.LoadScene("GameLost");
    }
    public void GameComplete()
    {
        PlayerPrefs.SetInt("point", point);
        SceneManager.LoadScene("GameWon");
    }
}
