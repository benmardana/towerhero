using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public int maxHealth = 3;
	int health = 3;

	public Canvas gameOverScreen;
	public int time = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0){
			// end game
			Instantiate(gameOverScreen);
			// not currently working - only for testing really
			// RestartTimer timer = (RestartTimer) gameOverScreen.gameObject.GetComponentInChildren(typeof(RestartTimer));
			// timer.SetRestartTimer(time);
			Scene loadedLevel = SceneManager.GetActiveScene();
     		StartCoroutine(ReloadScene(loadedLevel));
		}
	}

	IEnumerator ReloadScene(Scene level){
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene (level.buildIndex);
	}

	public void reduceHealth(int damage){
		health -= damage;
	}
}
