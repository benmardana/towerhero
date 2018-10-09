using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Text livesText;          // (lives remaining)
    public Text resourcesText;
    public Text waveText;
    public Text levelText;

    // Use this for initialization
    void Start () {
        Text[] texts = this.GetComponentsInChildren<Text>();

        foreach (Text text in texts) {
            if (text.tag == "LivesText") {
                livesText = text;
            } else if (text.tag == "ResourcesText") {
                resourcesText = text;
            } else if (text.tag == "WaveText") {
                waveText = text;
            } else if (text.tag == "LevelText") {
                levelText = text;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        livesText.text = "Lives: " + GameState.lives; 
        resourcesText.text = "Resources: " + ResourceManager.resources;             // TODO - requires resource manager (Adam)
        waveText.text = "Wave: " + GameState.waveNumber;
        levelText.text = "Level: " + GameState.levelNumber;
    }
}
