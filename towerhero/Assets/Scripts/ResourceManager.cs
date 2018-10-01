using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static int resources = 200;

    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // In future this will take an enemy type and determine amt.
    public static void EnemyIsKilled()
    {
        resources += 5;
    }

    // This will also take a turret type.
    public static void TurretBuilt()
    {
        resources -= 50;
    }

    public static void ResetResources()
    {
        resources = 200;
    }
}
