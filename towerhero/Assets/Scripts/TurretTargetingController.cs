/**
 * The following video was used as a guideline for turret tracking and rotation:
 * https://www.youtube.com/watch?v=QKhn2kl9_8I&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=4
 */

using UnityEngine;

public class TurretTargetingController : MonoBehaviour {

    public string enemyTag = "Enemy";

    private Transform target;
    private TurretController turret;        // the turret this is attached to

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 1.0f);
        turret = this.gameObject.GetComponent<TurretController>();
	}

	void Update () {

        // If we have no target do nothing
        if (target == null) {
            return;
        }

        Vector3 dir = target.position - this.transform.position;        // direction of target from turret
        Quaternion lookRotation = Quaternion.LookRotation(dir);         // how far we need to rotate
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turret.rotationSpeed);

        // to rotate with respect to one axis instead:
        //Vector3 rotation = lookRotation.eulerAngles;
        //this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);     // rotating about the y-axis

    }

    // Finds the closest target in range
    // Note: Not performed with every Update, since it is relatively costly.
    void UpdateTarget() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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

        if (nearestEnemy != null && minDistance <= turret.range) {
            this.GetComponent<WeaponController>().EnableWeapon();
            target = nearestEnemy.transform;
        } else {
            this.GetComponent<WeaponController>().DisableWeapon();
            target = null;
        }
    }
}
