using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SuccessScreenScript : MonoBehaviour {

    public const float WaitTime = 30f;

    // Wait, and then reload the main menu scene
    void Start () {

        StartCoroutine(LoadMenuDelayed());
    }

    IEnumerator LoadMenuDelayed() {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}
