using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {
   
	public Canvas gameOverScreen;
	public int time = 5;

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
	}

	IEnumerator ReloadScene(Scene level){
		// wait for the set time
		yield return new WaitForSeconds(time);
        GameState.ResetGameState();
		SceneManager.LoadScene (level.buildIndex);
	}

    // for each enemy which gets to the goal, reduce lives
    public void reduceLives(){
        GameState.lives -= 1;
	}
}
