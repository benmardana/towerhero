using System.Collections;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {
	private int _waves = GameState.FinalWave;
	public Transform[] EnemyUnits;
	private int _waveCount = 1;
	
	public int SecondsBetweenWaves;
	
	public int EnemyGroupsPerWave;
	private int _enemyGroupsPerWaveCount = 0;
	
	public int SecondsBetweenGroupsInWave;

	private bool _paused = false;

	// Use this for initialization
	void Start ()
	{
		// start spawning groups
		InvokeRepeating("SpawnWave", 1.0f, SecondsBetweenGroupsInWave);
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameState.waveNumber = _waveCount;

		// if we've spawned enough per group
		if (!_paused && _enemyGroupsPerWaveCount >= EnemyGroupsPerWave)
		{
			// pause the spawning
			_paused = true;
			StartCoroutine(Pause());
		}
		
		// if we've spawned all of the waves
		if (_waveCount >= _waves)
		{
            // cancel any lingering invokes and coroutines to prevent async calls to spawn more enemies
            CancelInvoke();
			StopAllCoroutines();
            _waveCount++;
            
        }
	}
	
	IEnumerator Pause() {
		// stop spawning any enemies
		CancelInvoke();
		// wait
		yield return new WaitForSeconds(SecondsBetweenWaves);
		// continue spawning enemies
		if (_waveCount < _waves)
		{
			InvokeRepeating("SpawnWave", 0.0f, SecondsBetweenGroupsInWave);
			_waveCount++;
			_enemyGroupsPerWaveCount = 0;
			_paused = false;
		}
	}

	void SpawnWave()
	{
		SpawnEnemyGroup();
		_enemyGroupsPerWaveCount++;
	}

	void SpawnEnemyGroup() {
		foreach(Transform enemy in EnemyUnits) {
			Vector3 spawnPoint = RandomPointInXZBounds(GetComponent<MeshCollider>().bounds);
			Instantiate(enemy, spawnPoint, Quaternion.identity);
		}		
	}

	static Vector3 RandomPointInXZBounds(Bounds bounds) {
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			0,
			Random.Range(bounds.min.z, bounds.max.z)
		);
	}
}
