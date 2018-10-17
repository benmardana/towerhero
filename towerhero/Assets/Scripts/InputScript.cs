using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {
    
    ClickController clickController;
    WeaponController weaponController;
	public GameObject[] Turrets;
    private GameObject selectedTurret;
    private String turretIndex;
	
	public Button PurpleButton;
	public Button RedButton;

	public Button _frostBiteButton;
	public Button _warlockFlameButton;
	public String _selectedAbility;

	private GameObject _freezeAbility;
	private Light _freezeTarget;
	private MeshCollider _FreezeTargetArea;
	public int FreezeAbilityModifier = 10;
	public int FreezeAbilityDuration = 5;
	
	
	private GameObject _greenFlameAbility;
	private Light _greenFlameTarget;
	private MeshCollider _greenFlameTargetArea;
	
	private GameObject _towerTracker;
	private Light _towerTarget;


	// assign all slave scripts in Start()
    void Start () {
	    
	    
		_freezeAbility = GameObject.Find("FreezeAbility");
		_freezeTarget = _freezeAbility.GetComponent<Light>();
		_freezeTarget.enabled = false;
		_FreezeTargetArea = _freezeAbility.GetComponentInChildren<MeshCollider>();
	    
	    _greenFlameAbility = GameObject.Find("GreenFireAbility");
	    _greenFlameTarget = _greenFlameAbility.GetComponent<Light>();
	    _greenFlameTarget.enabled = false;

	    _towerTracker = GameObject.Find("TowerTracker");
	    _towerTarget = _towerTracker.GetComponent<Light>();
	    
        PurpleButton.onClick.AddListener(ToggleTurretPurple);
        RedButton.onClick.AddListener(ToggleTurretRed);

	    ToggleTurretPurple();

    }
	
	public void Update () {

		// Freeze Ability
		if (Input.GetKey(KeyCode.E) && Cooldown.coolingDown == false)
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

				if (Input.GetButtonDown("Fire1"))
				{
                    Cooldown.coolingDown = true;
					var enemies = GameObject.FindGameObjectsWithTag("Enemy");
					foreach (var enemy in enemies)
					{
						var eCollider = enemy.GetComponent<MeshCollider>();
						if ((eCollider != null) && _FreezeTargetArea.bounds.Intersects(eCollider.bounds))
						{
							// slows down targets
							// SlowDown(multiplier, duration)
							enemy.GetComponent<EnemyAIScript>().SlowDown(FreezeAbilityModifier, FreezeAbilityDuration);
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
		
		// Green Flame Ability
		if (Input.GetKey(KeyCode.R) && Cooldown.coolingDown == false)
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			
			if (terrainHits.Any())
			{
				var hit = terrainHits.First();
				_greenFlameTarget.enabled = true;
				var greenFlameTargetPosition = _greenFlameAbility.transform.position;
				var greenFlameTargetPositionY = greenFlameTargetPosition.y;
				var greenFlameTargetPositionX = hit.point.x;
				var greenFlameTargetPositionZ = hit.point.z;
				greenFlameTargetPosition = new Vector3(greenFlameTargetPositionX, greenFlameTargetPositionY, greenFlameTargetPositionZ);
				_greenFlameAbility.transform.position = greenFlameTargetPosition;

				if (Input.GetButtonDown("Fire1"))
				{
					var greenCannons = _greenFlameAbility.GetComponentsInChildren<FireAbilityScript>();
					foreach (var cannon in greenCannons)
					{
						cannon.Spray();
					}
				}
			}
			else
			{
				_greenFlameTarget.enabled = false;
			}
		}
		else
		{
			_greenFlameTarget.enabled = false;
		}
		
		// if input is detected
		// check what it is then call the relevant function from the slave script
		if (Input.GetKey(KeyCode.Escape)) {
			// pause menu camera
			// TODO
		}

		
		// place turret selected
		if (Input.GetButton("Fire1"))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
			var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
		
			if (placeableHits.Any() && !nonPlaceableHits.Any())
			{
				var hit = terrainHits.First();
				_towerTarget.enabled = true;
				_towerTarget.color = turretIndex == "Red" ? Color.red : Color.magenta;
				var _towerTargetPosition = _freezeAbility.transform.position;
				var _towerTargetPositionY = _towerTargetPosition.y;
				var _towerTargetPositionX = hit.point.x;
				var _towerTargetPositionZ = hit.point.z;
				_towerTargetPosition = new Vector3(_towerTargetPositionX, _towerTargetPositionY, _towerTargetPositionZ);
				_towerTracker.transform.position = _towerTargetPosition;
			}
			else
			{
				_towerTarget.enabled = false;
			}
		}
		else
		{
			_towerTarget.enabled = false;
		}
		
		if (Input.GetButtonUp("Fire1"))
		{
			_towerTarget.enabled = false;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
			var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
			var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
			var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
			if (ResourceManager.resources >= 50)
			{
				if (placeableHits.Any() && !nonPlaceableHits.Any())
				{
					var hit = terrainHits.First();
					Vector3 instantiationPoint = new Vector3(hit.point.x + 1.6f,
						hit.point.y + selectedTurret.transform.position.y, hit.point.z);
					Instantiate(selectedTurret, instantiationPoint, Quaternion.identity);
					ResourceManager.TurretBuilt(turretIndex);
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
				ResourceManager.ReturnResources(turretIndex);
			}
		}

	}

    public void ToggleTurretRed()
    {
	    GameObject.FindWithTag("highlight").transform.position
		    = RedButton.transform.position;
	    GameObject.FindWithTag("highlight").GetComponent<Image>().enabled = true;
        turretIndex = "Red";
        selectedTurret = Turrets[1];
    }

    public void ToggleTurretPurple()
    {
	    GameObject.FindWithTag("highlight").transform.position
		    = PurpleButton.transform.position;
	    GameObject.FindWithTag("highlight").GetComponent<Image>().enabled = true;
        turretIndex = "Purple";
        selectedTurret = Turrets[0];
    }
}
