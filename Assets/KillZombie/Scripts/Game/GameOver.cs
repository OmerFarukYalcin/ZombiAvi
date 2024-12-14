using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text point; // UI Text element to display the player's score

    void Start()
    {
        // Show the cursor when the game is over
        Cursor.visible = true;

        // Retrieve the player's score from PlayerPrefs and display it
        point.text = "Puanınız: " + PlayerPrefs.GetInt("point");
    }

    // Method to reload the game scene when the player chooses to try again
    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
