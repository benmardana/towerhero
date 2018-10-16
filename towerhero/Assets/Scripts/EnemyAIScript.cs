using UnityEngine;

public class EnemyAIScript : MonoBehaviour {

	GameObject goal;
	UnityEngine.AI.NavMeshAgent AIAgent;
	private float _speed;

	void Start() {
		AIAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

		// give a random speed for some dynamics
		// do not amend angular speed or acceleration
		// keep at 999 as shown in unity editor
		// lower than that they get v confused in tight corners
		_speed = AIAgent.speed = Random.Range(1f, 2f) * Options.enemyMovementSpeedMultiplier;
		goal = GameObject.FindWithTag("EnemyGoal");
		Animate();
	}

	void Animate() {
		AIAgent.SetDestination(goal.transform.position);
	}

	public void SlowDown(int multiplier, int duration)
	{
		AIAgent.speed /= multiplier;
		Invoke("Slow", duration);
	}
	
	void Slow() {
		AIAgent.speed = _speed;
	}
}
