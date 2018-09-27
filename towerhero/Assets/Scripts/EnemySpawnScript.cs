using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	public float groupSpawnPeriodS = 10.0f;
	public int groupSize = 5;
	int spawned = 0;

	// list enables multiple enemy prefabs to be dropped in for larger crowds
	// doing more than 2 can cause some clipping issues though
	public Transform[] enemyUnits;

	// After 1 second, begin spawning enemies at regular intervals
	void Start () {
		InvokeRepeating("SpawnEnemies", 1.0f, groupSpawnPeriodS);
	}
	
	void Update () {
		// if 5 sets have been spawned, stop spawning any more
		if (spawned >= 5){
			StopAllCoroutines();
			spawned = 0;
		}
	}

	void SpawnEnemies(){
		StartCoroutine(Spawn());
	}

	// spawns enemies every second
	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(1f);
		spawned += 1;
		SpawnEnemy();
		StartCoroutine(Spawn());
	}

	// will spawn enemies at random points in a given bounding box
	// strictly only works on regular quadrilateral boxes
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
