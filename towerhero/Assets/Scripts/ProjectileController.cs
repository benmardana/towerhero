/**
 * Code modified from Graphics and Interaction Labs 6 & 7.
 */

using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private Vector3 unitVelocity;

    public float speed = 1.0f;
    public int damageAmount = 50;
    
    void Update() {
        this.gameObject.transform.Translate(unitVelocity * speed * Time.deltaTime);
    }

    // Handle collisions
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Enemy") {
            // Damage object with relevant tag
            EnemyHealth health = col.gameObject.GetComponent<EnemyHealth>();
            health.DamageEnemy(damageAmount);

            // Destroy self (i.e. the projectile)
            Destroy(this.gameObject);
        }
    }

    public void setUnitVelocity(Vector3 unitVelocity) {
        this.unitVelocity = unitVelocity;
    }
}
