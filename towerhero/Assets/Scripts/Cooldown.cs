using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements

// inspiration taken from this video: https://www.youtube.com/watch?v=8Xgao1qP7xw

public class Cooldown : MonoBehaviour
{
    public Image frostbite;
    public Image greenability;
    public static bool coolingDownFrost;
    public static bool coolingDownGreen;
    public float frostWaitTime = 15.0f;
    public float greenWaitTime = 30.0f;

    // Update is called once per frame
    void Update()
    {
        if (coolingDownFrost)
        {
            //Reduce fill amount over 15 seconds
            frostbite.fillAmount += 1.0f / frostWaitTime * Time.deltaTime;

            if (frostbite.fillAmount >= 1)
            {
                frostbite.fillAmount = 0;
                coolingDownFrost = false;
            }
        }

        if (coolingDownGreen)
        {
            greenability.fillAmount += 1.0f / greenWaitTime * Time.deltaTime;

            if (greenability.fillAmount >= 1)
            {
                greenability.fillAmount = 0;
                coolingDownGreen = false;
            }
        }
    }
  
    public static void ResetCooldowns()
    {
        coolingDownGreen = false;
        coolingDownFrost = false;
    }
   
}