using UnityEngine;

public class ClickController : MonoBehaviour {

	// not currently attached to anything
	// this script casts a ray from the camera and then if it hits something
	// in this case WorldTerrain, does something with it
	// in this example I was using it to set the destination of player if using
	// navmesh agent on player
	
	GameObject player;
	GameObject[] enemies;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

	void OnMouseDown() {

		if (player != null && enemies != null) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
			RaycastHit hit = new RaycastHit();

			if (Physics.Raycast (ray, out hit)) {
				Debug.Log(hit.collider.gameObject.name);
				Debug.Log(hit.point);			

				if (hit.collider.gameObject.name == "WorldTerrain") {
					foreach(GameObject enemy in enemies) {
						enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
					}
				} else {
					player.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
				}
			}
		}
		
    }
}
