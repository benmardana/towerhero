using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - Adam, I've only done some base stuff so that I can make the targeting controller
public class TurretController : MonoBehaviour {

    public float range = 15;
    public float rotationSpeed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Visually shows the range of the turret - for editing / debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    public void handleTurrets()
    {
        
    }
}
