using UnityEngine;
using System.Collections;

public class LeaderBehavior : AgentBehavior {

	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 previousPosition = transform.position;
		influences = transform.forward;
		influences += GetWaveInfluence();
		
		Vector3 currentPosition = transform.position + influences * Time.deltaTime * speed;
		rigidbody.MovePosition(currentPosition);
		transform.rotation = Quaternion.LookRotation(transform.forward);
		velocity = currentPosition - previousPosition;
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position, transform.position + velocity);
	}
}
