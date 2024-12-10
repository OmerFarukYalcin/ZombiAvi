using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text point;
    void Start()
    {
        Cursor.visible = true;
        point.text = "Puan�n�z:" + PlayerPrefs.GetInt("point");
    }

    // Update is called once per frame
    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
