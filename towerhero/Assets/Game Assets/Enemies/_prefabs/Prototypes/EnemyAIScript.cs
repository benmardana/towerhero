using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour {

	GameObject goal;
	UnityEngine.AI.NavMeshAgent AIAgent;

	void Awake() {
		gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
		AIAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Start () {
		goal = GameObject.FindWithTag("EnemyGoal");
		AIAgent.SetDestination(goal.transform.position);
	}
}
