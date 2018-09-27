using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour {
     public float speed = 3.5f;
     private float X;
     private float Y;
	public void HandlePan(){
		Transform cam = GameObject.FindWithTag("CameraCentre").transform;
		cam.Rotate(-new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
		X = cam.eulerAngles.x;
		Y = cam.eulerAngles.y;
		cam.rotation = Quaternion.Euler(X, Y, 0);
	}

}
