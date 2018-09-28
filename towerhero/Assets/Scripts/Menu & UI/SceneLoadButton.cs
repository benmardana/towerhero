using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour {

    public Object sceneToLoad;

	public void GoToScene()
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}
