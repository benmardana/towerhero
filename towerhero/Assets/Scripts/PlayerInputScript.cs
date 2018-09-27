using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour {

	PlayerMovementScript movementScript;
	PlayerCameraScript cameraScript;

	// assign all slave scripts in Start()
	void Start () {
		movementScript = GetComponent<PlayerMovementScript>();
		cameraScript = GetComponent<PlayerCameraScript>();
	}
	
	void Update () {
		// if input is detected
		// check what it is then call the relevant function from the slave script
		if (Input.anyKey) {
			if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
				// move player
				movementScript.HandleMovement();
			}
			if(Input.GetKey(KeyCode.Mouse1)) {
				// pan camera
				cameraScript.HandlePan();
         	}
			// if (Input.GetButtonDown("Fire1")) {
			// 	// shoot weapons
			// 	HandleWeapons();
			// }
			// // arbitrarily chose if you hold down t and click then that is a turret placement
			// if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.T)) {
			// 	// place turrets
			// 	HandleTurrets();
			// }
			// if (Input.GetKey(KeyCode.Escape)) {
			// 	// pause etc
			// 	HandleMisc();
			// }
		}
	}
}
