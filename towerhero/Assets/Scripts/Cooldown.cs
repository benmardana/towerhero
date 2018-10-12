using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class Cooldown : MonoBehaviour
{
    public Image frostbite;
    public static bool coolingDown;
    public float waitTime = 15.0f;

    // Update is called once per frame
    void Update()
    {
        if (coolingDown)
        {
            //Reduce fill amount over 30 seconds
            frostbite.fillAmount += 1.0f / waitTime * Time.deltaTime;

            if (frostbite.fillAmount >= 1)
            {
                frostbite.fillAmount = 0;
                coolingDown = false;
            }
        }


    }
}