using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour {
    
	CameraScript cameraScript;
    ClickController clickController;
    WeaponController weaponController;

	// assign all slave scripts in Start()
	void Start () {
		cameraScript = GetComponent<CameraScript>();
        clickController = GetComponent<ClickController>();      // TODO - not being used?
	}
	
	public void Update () {
		// if input is detected
		// check what it is then call the relevant function from the slave script
		if (Input.anyKey) {
			if(Input.GetKey(KeyCode.Mouse1)) {
				// pan camera
				cameraScript.HandlePan();
         	}
            if (Input.GetKey(KeyCode.Escape)) {
                // pause menu camera
                // TODO
            }
            // arbitrarily chose if you hold down t and click then that is a turret placement
            // TODO (Adam) - neaten up, too many nested for loops, is hard to understand
            // TODO (Adam) - comments
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

        }

	}
}
