using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour {

	public float speed = 3.5f;
	private float X;
	private float Y;

	// pans camera around by rotating the central rig of the camera (note three part camera in unity)
	public void HandlePan(){
		Transform cam = GameObject.FindWithTag("CameraCentre").transform;
		// rotates camera
		cam.Rotate(-new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));

		// keeps it level
		X = cam.eulerAngles.x;
		Y = cam.eulerAngles.y;
		cam.rotation = Quaternion.Euler(X, Y, 0);
	}

}
