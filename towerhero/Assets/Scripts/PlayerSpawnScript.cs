using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour {

	public Transform player;

	void Start () {
		Instantiate(player, this.transform.position, Quaternion.identity);
	}
	}
