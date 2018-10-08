using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{

	public Transform ParentProjectile;

	private ParticleSystem _particleSystem;

	// Use this for initialization
	void Start ()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_particleSystem.startRotation = ParentProjectile.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
	}
}
