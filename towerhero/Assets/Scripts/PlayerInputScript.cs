using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour {

	PlayerMovementScript movementScript;
	PlayerCameraScript cameraScript;
    ClickController clickController;
    WeaponController weaponController;

	// assign all slave scripts in Start()
	void Start () {
		movementScript = GetComponent<PlayerMovementScript>();
		cameraScript = GetComponent<PlayerCameraScript>();
        //weaponController = GetComponent<WeaponController>();
        clickController = GetComponent<ClickController>();
	}
	
	public void Update () {
		// if input is detected
		// check what it is then call the relevant function from the slave script
		if (Input.anyKey) {
			if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
				// move player
				movementScript.HandleMovement();
			}
			if(Input.GetKey(KeyCode.Mouse1)) {
				// pan camera
				cameraScript.HandlePan();
         	}
            if (Input.GetKey(KeyCode.Escape)) {
                // pause menu camera
                // TODO
            }
            //if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //if (Input.GetKeyDown(KeyCode.F)) { 
            //    // shoot weapons
            //    weaponController.Shoot();
            //}
            // arbitrarily chose if you hold down t and click then that is a turret placement
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.T))
            {
                if (ResourceManager.resources >= 50)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        if (hit.collider.tag == "Wall")
                        {
                            GameObject turret = (GameObject) Resources.Load("turret");
                            // This ensures the turret is loaded 'above' the wall.
                            Vector3 instantiationPoint = new Vector3(hit.point.x, hit.point.y + turret.transform.position.y, hit.point.z);
                            Instantiate(turret, instantiationPoint, Quaternion.identity);
                            ResourceManager.TurretBuilt();
                        }
                    }
                }
            }

            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.X))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.collider.tag == "Turret" || hit.collider.tag == "Bridge")
                    {
                        Destroy(hit.collider.gameObject);
                        ResourceManager.ReturnResources();
                    }
                }
            }

            // if (Input.GetKey(KeyCode.Escape)) {
            // 	// pause etc
            // 	HandleMisc();
            // }
        }
	}
}
