using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public int maxHealth = 3;
	int health = 3;

	public Canvas gameOverScreen;
	public int time = 5;

	void Start () {
		
	}
	
	void Update () {
		if (health <= 0){
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
		SceneManager.LoadScene (level.buildIndex);
	}

	public void reduceHealth(int damage){
		health -= damage;
	}
}
