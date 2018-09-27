using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour {

	GameObject goal;
	UnityEngine.AI.NavMeshAgent AIAgent;

	void Start() {
		AIAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		AIAgent.speed = Random.Range(6f, 7f);
		goal = GameObject.FindWithTag("EnemyGoal");
		Animate();
	}

	void Animate(){
		AIAgent.SetDestination(goal.transform.position);
	}
}
