using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO - firerate, this only applies to turrets not to heros? also depends on weapon type...
// Note: My current design assumes all weapons are automatic (i.e. have a firerate)
public class WeaponController : MonoBehaviour {

    public GameObject projectilePrefab;
    public float firerate;              // shots per second
    private Vector3 unitDirection;     // direction the gun is aiming in
    private float shotPeriod;           // time between shots (in seconds)
    private float timeSinceLastShot;
    private bool weaponEnabled;         // if the weapon is actively shooting

	// Use this for initialization
	void Start () {
        unitDirection = this.gameObject.transform.forward.normalized;
        shotPeriod = 1.0f / firerate;
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

        // Updating shooting direction
        unitDirection = this.gameObject.transform.forward.normalized;
    }

    // Fire the weapon
    public void Shoot() {

        // Instantiate a projectile
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = this.gameObject.transform.position;
        projectile.GetComponent<ProjectileController>().setUnitVelocity(unitDirection);


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
