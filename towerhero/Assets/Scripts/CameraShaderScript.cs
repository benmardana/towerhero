using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderScript : MonoBehaviour {

    public Shader shader;
    
	void Start () {
        GetComponent<Camera>().SetReplacementShader(shader, "");
    }
}
