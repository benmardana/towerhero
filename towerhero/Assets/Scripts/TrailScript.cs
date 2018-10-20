using UnityEngine;

public class TrailScript : MonoBehaviour
{

	public Transform _parentProjectile;

	private ParticleSystem _particleSystem;

	// Use this for initialization
	void Start ()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// ignores deprecated compiler warning
#pragma warning disable 618
		_particleSystem.startRotation = _parentProjectile.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
#pragma warning restore 618
	}
}
