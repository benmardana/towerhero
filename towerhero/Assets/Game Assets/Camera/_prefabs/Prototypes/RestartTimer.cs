using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartTimer : MonoBehaviour {

	Text text;
	int countdown;

	void Start() {
		text = GetComponent<Text>();
		text.text = string.Format("Game Over!\nGame restarting in 5 seconds");
		// InvokeRepeating("Countdown", 1f, 1f);
	}
	// Use this for initialization
	public void SetRestartTimer(int time){
		countdown = time;
	}

	void Countdown(){
		countdown -= 1;
	}
}
