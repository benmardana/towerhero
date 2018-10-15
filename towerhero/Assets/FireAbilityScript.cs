using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbilityScript : MonoBehaviour
{

	private int _fireballCapacity;
	private int _fireballs = 0;
	private float _rate;
	
	public GameObject FireAbilityProjectile;

	void Start()
	{
		_fireballCapacity = Random.Range(1,3);
		_rate = Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (_fireballs >= _fireballCapacity)
		{
			CancelInvoke();
		}
	}

	public void Spray()
	{
		InvokeRepeating("Shoot", 0.0f, _rate);
	}

	public void Reset()
	{
		_fireballs = 0;
	}
	
	// Fire the weapon
	public void Shoot() {

		// Instantiate a projectile
		Instantiate(FireAbilityProjectile, transform.position + transform.forward, transform.rotation);
		_fireballs++;
	}
}
