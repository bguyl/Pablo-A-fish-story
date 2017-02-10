using UnityEngine;

public class BullshitScript : MonoBehaviour {

	public GameObject boid;

	void Update () {
		transform.position = new Vector3(boid.transform.position.x, transform.position.y, boid.transform.position.z);
	}
}
