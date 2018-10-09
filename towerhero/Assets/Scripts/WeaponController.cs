using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


// TODO - firerate, this only applies to turrets not to heros? also depends on weapon type...
// Note: My current design assumes all weapons are automatic (i.e. have a firerate)
public class WeaponController : MonoBehaviour {

    public GameObject projectilePrefab;
    [FormerlySerializedAs("firerate")] public float Firerate;              // shots per second
    private float shotPeriod;           // time between shots (in seconds)
    private float timeSinceLastShot;
    private bool weaponEnabled;         // if the weapon is actively shooting

	// Use this for initialization
	void Start () {
        shotPeriod = 1.0f / Firerate;
        DisableWeapon();
        timeSinceLastShot = 0.0f;
	}

	void Update () {

        // Checks if next shot should be made, and makes it
        timeSinceLastShot += Time.deltaTime;
        if (weaponEnabled && timeSinceLastShot > shotPeriod) {
            Shoot();
            timeSinceLastShot = 0.0f;
        }
    }

    // Fire the weapon
    public void Shoot() {

        // Instantiate a projectile
        Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);


        // TODO (Optional) Smoke / etc effect visible at end of weapon barrel

    }

    // If turret is targeting an enemy, should be set to true
    public void EnableWeapon() {
        weaponEnabled = true;
    }

    // If turret is not targeting an enemy (none in sight / range), should be set to false
    public void DisableWeapon() {
        weaponEnabled = false;
    }
}
