using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    ResourceManager resourceManager;

    public int initialHealth = 100; // varies for different enemies
    // TODO public GameObject createOnDestroy;  // something to create when enemy is destroyed

	private int currentHealth;

    void Start() {
        resourceManager = GetComponent<ResourceManager>();
        currentHealth = initialHealth;
    }

    // Damage enemy
    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        // If health is depleted, delete enemy and increment Player's resources
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
            ResourceManager.EnemyIsKilled();
        }
    }

    public int GetCurrentHealth() {
		return currentHealth;
	}
}
