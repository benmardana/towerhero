using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int initialHealth = 100; // varies for different enemies
    // TODO public GameObject createOnDestroy;  // something to create when enemy is destroyed

	private int currentHealth;

    void Start() {
        currentHealth = initialHealth;
    }

    // Damage enemy
    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        // If health is depleted, delete enemy and increment Player's resources
        if (currentHealth <= 0) {
            Destroy(this.gameObject);

            // TODO - increment player's resources (using ResourceManager)
        }
    }

    public int GetCurrentHealth() {
		return currentHealth;
	}
}
