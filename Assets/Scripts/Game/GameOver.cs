using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text point;
    void Start()
    {
        Cursor.visible = true;
        point.text = "Puanýnýz:" + PlayerPrefs.GetInt("puan");
    }

    // Update is called once per frame
    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
