using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public int maxHealth = 3;
	int health = 3;

	public Canvas gameOverScreen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0){
			// end game
			Instantiate(gameOverScreen);
		}
	}
}
