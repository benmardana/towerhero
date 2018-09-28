using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySliderController : MonoBehaviour {

    void Start()
    {
        GameObject.Find("DifficultySlider").GetComponent<Slider>().value = Options.difficulty;
    }

	public void OnSliderInteraction()
    {
        Options.difficulty = GameObject.Find("DifficultySlider").GetComponent<Slider>().value;
    }
}
