using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float moveSpeed = 4f;

	Vector3 forward, right;

	GameObject m_Camera;

	void Start() {
		m_Camera = GameObject.FindWithTag("MainCamera");
	}

	void Update() {
		ResetRelativeDirection();
	}

    public void HandleMovement(){
		// note that player is prevented from running off the edge of the platforms
		// by invisible walls in the scene

		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

		Vector3 heading = Vector3.Normalize(direction);
		if (heading != Vector3.zero){
			transform.forward = heading;
			transform.position += rightMovement;
			transform.position += upMovement;
		}
	}

	// This updates the 'forward' transform of the object relative to the current camera direction
	void ResetRelativeDirection(){
		forward = m_Camera.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}
}
