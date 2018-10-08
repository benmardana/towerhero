/**
 * Code modified from Graphics and Interaction Labs 6 & 7.
 */

using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float Speed = 1.0f;
    public int DamageAmount = 50;
    
    void Update() {
//        this.gameObject.transform.Translate(unitVelocity * speed * Time.deltaTime);
        transform.position += Time.deltaTime * Speed * transform.forward;
    }

    // Handle collisions
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Enemy") {
            // Damage object with relevant tag
            EnemyHealth health = col.gameObject.GetComponent<EnemyHealth>();
            health.DamageEnemy(DamageAmount);

            // Destroy self (i.e. the projectile)
            Destroy(this.gameObject);
        }
    }
}
