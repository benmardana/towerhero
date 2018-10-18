using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

    public Button resume;

    public Transform canvas;

    private void Start()
    {

        Button resumeButton = resume.GetComponent<Button>();
        resumeButton.onClick.AddListener(Pause);
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
  
    }
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
