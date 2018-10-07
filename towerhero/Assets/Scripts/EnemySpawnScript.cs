using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

    private const int InitNumberOfGroups = 3;
    private const float TimeBetweenWaves = 15f;

	public float groupSpawnPeriod = 5.0f;

	private int numberOfGroups = InitNumberOfGroups + GameState.waveNumber;
	private int numberOfGroupsSpawned = 0;    

	// list enables multiple enemy prefabs to be dropped in for larger crowds
	// doing more than 2 can cause some clipping issues though
    // Note: Units dropped in via the editor
    // Note2: Number of enemies spawned per wave == enemyUnits.length() * numberOfGroups
	public Transform[] enemyUnits;

	// After 1 second, begin spawning enemies at regular intervals
	void Start () {
		InvokeRepeating("SpawnEnemies", 1.0f, groupSpawnPeriod);
	}
	
	void Update () {

		// if numberOfGroups groups been spawned, stop spawning any more
		if (numberOfGroupsSpawned >= numberOfGroups) {
            // Stop all active Spawn() coroutines
            StopAllCoroutines();

            // Stop the current wave
            numberOfGroupsSpawned = 0;
            CancelInvoke("SpawnEnemies");
            StartCoroutine(Pause());

		}
	}

    // move to the next wave after waiting
    IEnumerator Pause() {
        yield return new WaitForSeconds(TimeBetweenWaves);
        InvokeRepeating("SpawnEnemies", 1.0f, groupSpawnPeriod);
        GameState.waveNumber++;
    }

    public void resetNumberOfGroupsSpawned() {
        numberOfGroupsSpawned = 0;
    }

	void SpawnEnemies() {
		StartCoroutine(Spawn());
	}

	// spawns enemies every second
    // (runs through and starts another coroutine before dieing)
	IEnumerator Spawn() {
		yield return new WaitForSeconds(1f);
        numberOfGroupsSpawned++;
		SpawnEnemy();
		StartCoroutine(Spawn());
	}

	// will spawn enemies at random points in a given bounding box
	// strictly only works on regular quadrilateral boxes
	void SpawnEnemy() {
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
