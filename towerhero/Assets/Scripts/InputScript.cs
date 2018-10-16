using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {
    
	CameraScript cameraScript;
    ClickController clickController;
    WeaponController weaponController;
	public GameObject[] Turrets;
    private GameObject selectedTurret;
    private String turretIndex;
	
	public Button _purpleButton;
	public Button _redButton;

	private GameObject _freezeAbility;
	private Light _freezeTarget;
	private MeshCollider _FreezeTargetArea;
	public int FreezeAbilityModifier = 10;
	public int FreezeAbilityDuration = 5;
	
	
	private GameObject _greenFlameAbility;
	private Light _greenFlameTarget;
	private MeshCollider _greenFlameTargetArea;


	// assign all slave scripts in Start()
    void Start () {
		cameraScript = GetComponent<CameraScript>();
        clickController = GetComponent<ClickController>();      // TODO - not being used?
	    
		_freezeAbility = GameObject.Find("FreezeAbility");
		_freezeTarget = _freezeAbility.GetComponent<Light>();
		_freezeTarget.enabled = false;
		_FreezeTargetArea = _freezeAbility.GetComponentInChildren<MeshCollider>();
	    
	    _greenFlameAbility = GameObject.Find("GreenFireAbility");
	    _greenFlameTarget = _greenFlameAbility.GetComponent<Light>();
	    _greenFlameTarget.enabled = false;
	    
        _purpleButton.onClick.AddListener(ToggleTurretPurple);
        _redButton.onClick.AddListener(ToggleTurretRed);

        turretIndex = "Purple";
        selectedTurret = Turrets[0];

       
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
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray.origin, ray.direction, 2000f);
                var terrainHits = hits.Where(x => x.collider.CompareTag("Terrain"));
                var placeableHits = hits.Where(x => x.collider.CompareTag("Placeable"));
                var nonPlaceableHits = hits.Where(x => x.collider.CompareTag("NonPlaceable"));
                var hit = terrainHits.First();

                if (ResourceManager.resources >= 50)
                {

	                if (placeableHits.Any() && !nonPlaceableHits.Any())
	                {

		                Vector3 instantiationPoint = new Vector3(hit.point.x + 1.6f, hit.point.y + selectedTurret.transform.position.y, hit.point.z);
		                Instantiate(selectedTurret, instantiationPoint, Quaternion.identity);
		                ResourceManager.TurretBuilt(turretIndex);
	                }
                }
            }

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

	}

    public void ToggleTurretRed()
    {
        turretIndex = "Red";
        selectedTurret = Turrets[1];
    }

    public void ToggleTurretPurple()
    {
        turretIndex = "Purple";
        selectedTurret = Turrets[0];
    }
}
