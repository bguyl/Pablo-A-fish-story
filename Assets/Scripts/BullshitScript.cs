using UnityEngine;

public class BullshitScript : MonoBehaviour {

	public GameObject boid;

	void Update () {
		transform.position = new Vector3(transform.position.x, boid.transform.position.y, boid.transform.position.z);
	}
}
