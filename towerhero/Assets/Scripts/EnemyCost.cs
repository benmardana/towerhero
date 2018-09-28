using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCost : MonoBehaviour {

	public int cost;

	// gets 'cost' of enemy, use when one dies and you want to bump up resources.
	public int getCost() {
		return cost;
	}
}
