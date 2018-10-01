using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If an enemy reaches the end goal, decrement number of player lives by 1 and 
// destroy the enemy.
public class DestroyAtEndScript : MonoBehaviour {
    
	void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "EnemyGoal") {
			// reduce lives
            LevelController endGoal = col.gameObject.GetComponent<LevelController>();
            endGoal.reduceLives();

            // Destroy self
            Destroy(this.gameObject);
        }
    }
}
