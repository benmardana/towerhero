using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float moveSpeed = 4f;

	Vector3 forward, right;

	GameObject m_Camera;

	void Start() {
		m_Camera = GameObject.FindWithTag("MainCamera");
		forward = m_Camera.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}

	void Update() {
		forward = m_Camera.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}

    public void HandleMovement(){
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
}
