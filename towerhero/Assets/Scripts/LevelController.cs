using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public const int ReloadWaitTime = 5;

    public Canvas gameOverScreen;

    void Start () {
	}
	
	void Update () {
		if (GameState.lives <= 0){
			// Load up Game Over Screen
			Instantiate(gameOverScreen);
			Scene loadedLevel = SceneManager.GetActiveScene();

			// Coroutines enable you to delay or schedule actions
     		StartCoroutine(ReloadScene(loadedLevel));
		}

        // Continually monitors the wave number
        if (GameState.waveNumber > GameState.FinalWave) {
            LoadNextLevel();
        }
	}

    // Load the level currently SET in GameState
    public static void LoadCurrentLevel() {
        SceneManager.LoadScene("Scenes/Levels/Level_" + GameState.levelNumber + "/level_" + GameState.levelNumber);
    }

    // Loads next level
    // Note: Last level / "Success" screen should be titled in the same format
    public static void LoadNextLevel() {
        GameState.SetNextLevelState();
        LoadCurrentLevel();
    }

	IEnumerator ReloadScene(Scene level) {

        // TODO - Prevent UI from updating (not working)
        //GameObject canvasObject = (GameObject) GameObject.FindObjectOfType(typeof(Canvas));
        //Text livesText = canvasObject.transform.Find("LivesText").GetComponent<Text>();
        //livesText.enabled = false;

        // Wait for the set time
        yield return new WaitForSeconds(ReloadWaitTime);

        //livesText.enabled = true;

        GameState.ResetGameState();
		SceneManager.LoadScene (level.buildIndex);
	}

    // for each enemy which gets to the goal, reduce lives
    public void reduceLives() {
        GameState.lives -= 1;
	}
}
