using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LeadingTargetingScript : MonoBehaviour {
	
	public string EnemyTag = "Enemy";
	
	//how fast our shots move
	float _shotSpeed;
	//objects
	GameObject _target;
 
	// === derived variables ===
	//positions
	private Vector3 _turretPosition;
	private Vector3 _targetPosition;
	//velocities
	private readonly Vector3 _turretVelocity = Vector3.zero;
	private Vector3 _targetVelocity;
 
	//calculate intercept
	private Vector3 _interceptPoint;
	
	private Vector3 _prevPos;
	private Vector3 _newPos;

	// Use this for initialization
	void Start () {
		UpdateTarget();
		_shotSpeed = GetComponent<WeaponController>().projectilePrefab.GetComponent<ProjectileController>().Speed;
		if (_target == null) return;
		_prevPos = _target.transform.position;
		_newPos = _target.transform.position;

	}
	
	void FixedUpdate()
	{
		if (_target == null) {
			return;
		}
		_newPos = _target.transform.position;  // each frame track the new position
		_targetVelocity = (_newPos - _prevPos) / Time.fixedDeltaTime;  // velocity = dist/time
		_prevPos = _newPos;  // update position for next frame calculation
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTarget();
		// If we have no target do nothing
		if (_target == null) {
			return;
		}
		
		_interceptPoint = FirstOrderIntercept
		(
			_turretPosition,
			_turretVelocity,
			_shotSpeed,
			_targetPosition,
			_targetVelocity
		);

		Vector3 dir = _interceptPoint - transform.position;
		float step = GetComponent<TurretController>().rotationSpeed * Time.deltaTime;
		
		Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);

	}
	
	void UpdateTarget() {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float minDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) {

			// Distance from turret to current target
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < minDistance) {
				minDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && minDistance <= GetComponent<TurretController>().range) {
			this.GetComponent<WeaponController>().EnableWeapon();
			_target = nearestEnemy;
		} else {
			this.GetComponent<WeaponController>().DisableWeapon();
			_target = null;
		}

		if (_target != null) _targetPosition = _target.transform.position;
		_turretPosition = transform.position;
	}
	
	public static Vector3 FirstOrderIntercept
	(
		Vector3 shooterPosition,
		Vector3 shooterVelocity,
		float shotSpeed,
		Vector3 targetPosition,
		Vector3 targetVelocity
	)  {
		Vector3 targetRelativePosition = targetPosition - shooterPosition;
		Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
		float t = FirstOrderInterceptTime
		(
			shotSpeed,
			targetRelativePosition,
			targetRelativeVelocity
		);
		return targetPosition + t*(targetRelativeVelocity);
	}
	
	public static float FirstOrderInterceptTime
	(
		float shotSpeed,
		Vector3 targetRelativePosition,
		Vector3 targetRelativeVelocity
	) {
		float velocitySquared = targetRelativeVelocity.sqrMagnitude;
		if(velocitySquared < 0.001f)
			return 0f;
 
		float a = velocitySquared - shotSpeed*shotSpeed;
 
		//handle similar velocities
		if (Mathf.Abs(a) < 0.001f)
		{
			float t = -targetRelativePosition.sqrMagnitude/
			          (
				          2f*Vector3.Dot
				          (
					          targetRelativeVelocity,
					          targetRelativePosition
				          )
			          );
			return Mathf.Max(t, 0f); //don't shoot back in time
		}
 
		float b = 2f*Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
		float c = targetRelativePosition.sqrMagnitude;
		float determinant = b*b - 4f*a*c;
 
		if (determinant > 0f) { //determinant > 0; two intercept paths (most common)
			float	t1 = (-b + Mathf.Sqrt(determinant))/(2f*a),
				t2 = (-b - Mathf.Sqrt(determinant))/(2f*a);
			if (t1 > 0f) {
				if (t2 > 0f)
					return Mathf.Min(t1, t2); //both are positive
				else
					return t1; //only t1 is positive
			} else
				return Mathf.Max(t2, 0f); //don't shoot back in time
		} else if (determinant < 0f) //determinant < 0; no intercept path
			return 0f;
		else //determinant = 0; one intercept path, pretty much never happens
			return Mathf.Max(-b/(2f*a), 0f); //don't shoot back in time
	}
}
