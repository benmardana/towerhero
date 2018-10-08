using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		transform.RotateAround(new Vector3(-0.6f, 1.25f, 1f), Vector3.up, 20 * Time.deltaTime);
	}
}
