using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    // Singleton instance of GameControl
    public static GameControl instance;

    // List to hold GameObjects tagged as "task"
    public List<GameObject> taskGoList = new();

    [SerializeField] private TaskManager taskManager; // Manages tasks in the game
    public GameObject zombie; // Prefab for the zombie to be spawned
    public Text pointText; // UI Text element for displaying points
    private int point; // Player's score
    private float createTime = 10f; // Time interval for spawning zombies
    [SerializeField] Obelisk obelisk; // Reference to the obelisk object
    [SerializeField] GameObject finish; // UI element shown when the game ends

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameControl exists
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        // Hide the cursor
        Cursor.visible = false;

        // Find all GameObjects tagged as "task" and populate the taskGoList
        taskGoList = GameObject.FindGameObjectsWithTag("task").ToList();

        // Initialize the TaskManager with the taskGoList
        taskManager = new(taskGoList);
    }

    // Coroutine to continuously spawn zombies at intervals
    IEnumerator Start()
    {
        while (!obelisk.gameComplete)
        {
            yield return new WaitForSeconds(createTime);
            CreateZombie();
        }
    }

    // Subscribing to the obelisk's game end event
    private void OnEnable()
    {
        obelisk.onGameEnded += HandleOnGameEnded;
    }

    // Unsubscribing from the obelisk's game end event
    private void OnDisable()
    {
        obelisk.onGameEnded -= HandleOnGameEnded;
    }

    // Handles the game ending event by activating the finish UI
    private void HandleOnGameEnded()
    {
        finish.SetActive(true);
    }

    private void Update()
    {
        // Exit the game when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    // Spawns a zombie at a random position within defined ranges
    private void CreateZombie()
    {
        Vector3 pos = new Vector3(Random.Range(204f, 343f), 22f, Random.Range(169f, 293f));
        Instantiate(zombie, pos, Quaternion.identity);
    }

    // Property to access the TaskManager
    public TaskManager TaskManager => taskManager;

    // Increases the player's points and updates the UI
    public void IncreasePoint(int p)
    {
        point += p;
        pointText.text = "" + point;
    }

    // Handles game over by saving the score and loading the GameLost scene
    public void GameOver()
    {
        PlayerPrefs.SetInt("point", point);
        SceneManager.LoadScene("GameLost");
    }

    // Handles game completion by saving the score and loading the GameWon scene
    public void GameComplete()
    {
        PlayerPrefs.SetInt("point", point);
        SceneManager.LoadScene("GameWon");
    }
}
