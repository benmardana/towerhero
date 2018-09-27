using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtEndScript : MonoBehaviour {

	public int damage = 1;

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyGoal")
        {
			// reduce health
            EndGame endGoal = col.gameObject.GetComponent<EndGame>();
            endGoal.reduceHealth(damage);

            // Destroy self
            Destroy(this.gameObject);
        }
    }
}
