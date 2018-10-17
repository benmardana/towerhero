using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class NewInputScript : MonoBehaviour
{
	ClickController clickController;
    WeaponController weaponController;
    public GameObject[] Turrets;
    
    public Button PurpleButton;
    public Button RedButton;
    public Button FrostBiteButton;
    public Button WarlockFlameButton;
    
    public int FreezeAbilityModifier = 10;
    public int FreezeAbilityDuration = 5;

    private GameObject _selectedPower;
	private String _powerName;
    private GameObject _powerTracker;
    private Light _powerTarget;
    
    void Start () {
	    
	    PurpleButton.onClick.AddListener(ToggleTurretPurple);
	    RedButton.onClick.AddListener(ToggleTurretRed);
	    FrostBiteButton.onClick.AddListener(ToggleFrostBite);
		WarlockFlameButton.onClick.AddListener(ToggleWarlockFlame);
	    
	    ToggleTurretPurple();
	    // highlight button
	    GameObject.FindWithTag("highlight").GetComponent<Image>().enabled = true;
	    // ensure position highlight off
	    _powerTracker.GetComponent<Light>().enabled = false;
    }

	private void Update()
	{
		
		if (Input.GetKey(KeyCode.Escape)) {
			// pause menu camera
			// TODO
		}
		
		if (Input.GetButton("Fire1") && canFire())
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
			var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
		
			if (canHit(terrainHits, placeableHits, nonPlaceableHits))
			{
				var hit = terrainHits.First();
				_powerTarget.enabled = true;
				var _powerTargetPosition = _powerTarget.transform.position;
				var _powerTargetPositionY = _powerTargetPosition.y;
				var _powerTargetPositionX = hit.point.x;
				var _powerTargetPositionZ = hit.point.z;
				_powerTargetPosition = new Vector3(_powerTargetPositionX, _powerTargetPositionY, _powerTargetPositionZ);
				_powerTracker.transform.position = _powerTargetPosition;
			}
			else
			{
				_powerTarget.enabled = false;
			}
		}
		else
		{
			_powerTarget.enabled = false;
		}
		
		if (Input.GetButtonUp("Fire1") && canFire())
		{
			_powerTarget.enabled = false;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
			var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
			if (ResourceManager.resources >= 50)
			{
				if (canHit(terrainHits, placeableHits, nonPlaceableHits))
				{
					var hit = terrainHits.First();
					if (_powerName == "Red" || _powerName == "Purple")
					{
						Vector3 instantiationPoint = new Vector3(hit.point.x + 1.6f,
							hit.point.y + _selectedPower.transform.position.y, hit.point.z);
						Instantiate(_selectedPower, instantiationPoint, Quaternion.identity);
						ResourceManager.TurretBuilt(_powerName);
					}
					else if (_powerName == "Warlock")
					{
						Cooldown.coolingDown = true;
						var cannons = _selectedPower.GetComponentsInChildren<FireAbilityScript>();
						foreach (var cannon in cannons)
						{
							cannon.Spray();
						}
					}
					else
					{
						Cooldown.coolingDown = true;
						var enemies = GameObject.FindGameObjectsWithTag("Enemy");
						foreach (var enemy in enemies)
						{
							var eCollider = enemy.GetComponent<MeshCollider>();
							if ((eCollider != null) && _selectedPower.GetComponentInChildren<MeshCollider>().bounds.Intersects(eCollider.bounds))
							{
								// slows down targets
								// SlowDown(multiplier, duration)
								enemy.GetComponent<EnemyAIScript>().SlowDown(FreezeAbilityModifier, FreezeAbilityDuration);
							}
						}
					}
				}
			}
		}

		// delete turret
		if (Input.GetKey(KeyCode.X) && Input.GetButtonDown("Fire1"))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var turretHits = hits.Where(x => x.collider.CompareTag("Turret"));
			if (turretHits.Any())
			{
				Destroy(turretHits.First().collider.gameObject);
				var turretDestroyed = turretHits.First().collider.gameObject.name == "PurpleTower"? "Purple" : "Red";
				ResourceManager.ReturnResources(turretDestroyed);
			}
		}
	}

	bool canFire()
	{
		return Cooldown.coolingDown || (_powerName == "Red" || _powerName == "Purple");
	}

	bool canHit(IEnumerable<RaycastHit> terrainHits, IEnumerable<RaycastHit> placeableHits, IEnumerable<RaycastHit> nonPlaceableHits)
	{
		if (_powerName == "Red" || _powerName == "Purple")
		{
			return placeableHits.Any() && !nonPlaceableHits.Any();
		}

		return terrainHits.Any();
	}

	public void ToggleTurretPurple()
	{
		// update button highlight
		GameObject.FindWithTag("highlight").transform.position
			= PurpleButton.transform.position;
		
		// update selected power
		_powerName = "Purple";
		_selectedPower = Turrets[0];
		
		// update tracker
		_powerTracker = GameObject.Find("TowerTracker");
		
		// update tracker light
		_powerTarget = _powerTracker.GetComponent<Light>();
		_powerTarget.color = Color.magenta;
	}
	
	public void ToggleTurretRed()
	{
		// update button highlight
		GameObject.FindWithTag("highlight").transform.position
			= RedButton.transform.position;
		
		// update selected power
		_powerName = "Red";
		_selectedPower = Turrets[1];
		
		// update tracker
		_powerTracker = GameObject.Find("TowerTracker");
		
		// update tracker light
		_powerTarget = _powerTracker.GetComponent<Light>();
		_powerTarget.color = Color.red;
	}

	public void ToggleFrostBite()
	{
		// update button highlight
		GameObject.FindWithTag("highlight").transform.position
			= FrostBiteButton.transform.position;
		// update selected power
		_powerName = "Frost";
		_selectedPower = GameObject.Find("FreezeAbility");
		
		// update tracker
		_powerTracker = _selectedPower;

		// update tracker light
		_powerTarget = _powerTracker.GetComponent<Light>();
	}
	
	public void ToggleWarlockFlame()
	{
		// update button highlight
		GameObject.FindWithTag("highlight").transform.position
			= WarlockFlameButton.transform.position;
		// update selected power
		_powerName = "Warlock";
		_selectedPower = GameObject.Find("GreenFireAbility");
		
		// update tracker
		_powerTracker = _selectedPower;
		
		// update tracker light
		_powerTarget = _powerTracker.GetComponent<Light>();
	}
	
	
}