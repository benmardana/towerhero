using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class InputScript : MonoBehaviour {
    
	CameraScript cameraScript;
    ClickController clickController;
    WeaponController weaponController;
	public GameObject[] Turrets;

	private GameObject _freezeAbility;
	private Light _freezeTarget;
	private MeshCollider _targetArea;

	// assign all slave scripts in Start()
	void Start () {
		cameraScript = GetComponent<CameraScript>();
        clickController = GetComponent<ClickController>();      // TODO - not being used?
		_freezeAbility = GameObject.Find("FreezeAbility");
		_freezeTarget = _freezeAbility.GetComponent<Light>();
		_freezeTarget.enabled = false;
		_targetArea = _freezeAbility.GetComponentInChildren<MeshCollider>();
	}
	
	public void Update () {

		// Player Ability highlighting area of effect
		if (Input.GetKey(KeyCode.E))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			
			if (terrainHits.Any())
			{
				var hit = terrainHits.First();
				_freezeTarget.enabled = true;
				var _freezeTargetPosition = _freezeAbility.transform.position;
				var _freezeTargetPositionY = _freezeTargetPosition.y;
				var _freezeTargetPositionX = hit.point.x;
				var _freezeTargetPositionZ = hit.point.z;
				_freezeTargetPosition = new Vector3(_freezeTargetPositionX, _freezeTargetPositionY, _freezeTargetPositionZ);
				_freezeAbility.transform.position = _freezeTargetPosition;

				if (Input.GetButtonDown("Fire1") && Cooldown.coolingDown == false)
				{
                    Cooldown.coolingDown = true;
					var enemies = GameObject.FindGameObjectsWithTag("Enemy");
					foreach (var enemy in enemies)
					{
						var eCollider = enemy.GetComponent<CapsuleCollider>();
						if (_targetArea.bounds.Intersects(eCollider.bounds))
						{
							// slows down targets
							// SlowDown(multiplier, duration)
							enemy.GetComponent<EnemyAIScript>().SlowDown(2, 5);
						}
					}
					
				}
			}
			else
			{
				_freezeTarget.enabled = false;
			}
		}
		else
		{
			_freezeTarget.enabled = false;
		}
		
		// if input is detected
		// check what it is then call the relevant function from the slave script
		// TODO - not sure this method is best practice. can refactor later though. optimisation not too important rn.
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
            if (Input.GetKey(KeyCode.T) && Input.GetButtonDown("Fire1"))
            {
                if (ResourceManager.resources >= 50)
                {
	                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	                var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
	                var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
	                var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
	                var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
	                if (placeableHits.Any() && !nonPlaceableHits.Any())
	                {
		                var hit = terrainHits.First();
		                Debug.Log(terrainHits.First().point);
		                GameObject turret = Turrets[0];
		                Vector3 instantiationPoint = new Vector3(hit.point.x + 1.6f, hit.point.y + turret.transform.position.y, hit.point.z);
		                Instantiate(turret, instantiationPoint, Quaternion.identity);
		                ResourceManager.TurretBuilt();
	                }
                }
            }

            if (Input.GetKey(KeyCode.X) && Input.GetButtonDown("Fire1"))
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
