using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public Button restart;
    public Button mainMenu;
    public const int ReloadWaitTime = 5;

    public Canvas gameOverScreen;
    private int _enemiesInScene;

    void Start()
    {
        Button button = restart.GetComponent<Button>();
        button.onClick.AddListener(RestartLevel);

        Button button1 = mainMenu.GetComponent<Button>();
        button1.onClick.AddListener(RestartGame);


    }

    void Update()
    {
        _enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (GameState.lives <= 0)
        {
            // Load up Game Over Screen
            Instantiate(gameOverScreen);
            Scene loadedLevel = SceneManager.GetActiveScene();

            // Coroutines enable you to delay or schedule actions
            StartCoroutine(ReloadScene(loadedLevel, ReloadWaitTime));
        }

        // Continually monitors the wave number
        if ((_enemiesInScene == 0) && (GameState.waveNumber >= GameState.FinalWave))
        {
            LoadNextLevel();
        }
    }

    // Load the level currently SET in GameState
    public static void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level_" + GameState.levelNumber + "/level_" + GameState.levelNumber);
    }

    // Loads next level
    // Note: Last level / "Success" screen should be titled in the same format
    public static void LoadNextLevel()
    {
        GameState.SetNextLevelState();
        LoadCurrentLevel();
    }

    IEnumerator ReloadScene(Scene level, int waitTime)
    {

        // TODO - Prevent UI from updating (not working)
        //GameObject canvasObject = (GameObject) GameObject.FindObjectOfType(typeof(Canvas));
        //Text livesText = canvasObject.transform.Find("LivesText").GetComponent<Text>();
        //livesText.enabled = false;

        // Wait for the set time
        yield return new WaitForSeconds(waitTime);

        //livesText.enabled = true;

        GameState.ResetGameState();
        SceneManager.LoadScene(level.buildIndex);
    }

    // for each enemy which gets to the goal, reduce lives
    public void reduceLives()
    {
        GameState.lives -= 1;
    }

    public void RestartLevel()
    {
        // Load up Game Over Screen
        Scene loadedLevel = SceneManager.GetActiveScene();

        // Coroutines enable you to delay or schedule actions
        StartCoroutine(ReloadScene(loadedLevel, 0));

        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        GameState.ResetGameState();
        SceneManager.LoadScene(1);

        Time.timeScale = 1;
    }
}
