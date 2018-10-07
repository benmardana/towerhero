using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour {

	GameObject goal;
	UnityEngine.AI.NavMeshAgent AIAgent;

	void Start() {
		AIAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

		// give a random speed for some dynamics
		// do not amend angular speed or acceleration
		// keep at 999 as shown in unity editor
		// lower than that they get v confused in tight corners
		AIAgent.speed = Random.Range(6f, 7f) * Options.enemyMovementSpeedMultiplier;
		goal = GameObject.FindWithTag("EnemyGoal");
		Animate();
	}

	void Animate() {
		AIAgent.SetDestination(goal.transform.position);
	}
}
