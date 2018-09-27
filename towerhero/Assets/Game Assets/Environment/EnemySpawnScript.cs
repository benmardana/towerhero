using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	public float groupSpawnPeriodS = 10.0f;
	public int groupSize = 5;
	int spawned = 0;
	public Transform enemyUnit;

	void Start () {
		InvokeRepeating("SpawnEnemies", 1.0f, groupSpawnPeriodS);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned >= 5){
			StopAllCoroutines();
		}
	}

	void SpawnEnemies(){
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(1f);
		spawned += 1;
		SpawnEnemy();
		StartCoroutine(Spawn());
	}

	void SpawnEnemy(){
		Instantiate(enemyUnit, this.transform.position, Quaternion.identity);
	}
}
