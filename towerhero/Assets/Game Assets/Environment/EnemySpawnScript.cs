using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	public float groupSpawnPeriodS = 10.0f;
	public int groupSize = 5;
	int spawned = 0;

	// list enables multiple enemy prefabs to be dropped in
	public Transform[] enemyUnits;

	void Start () {
		InvokeRepeating("SpawnEnemies", 1.0f, groupSpawnPeriodS);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned >= 5){
			StopAllCoroutines();
			spawned = 0;
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
		foreach(Transform enemy in enemyUnits) {
			Vector3 spawnPoint = RandomPointInXZBounds(this.GetComponent<MeshCollider>().bounds);
			Instantiate(enemy, spawnPoint, Quaternion.identity);
		}		
	}

	public static Vector3 RandomPointInXZBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        0,
        Random.Range(bounds.min.z, bounds.max.z)
    );
}
}
