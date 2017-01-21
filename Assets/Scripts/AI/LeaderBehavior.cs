using UnityEngine;
using System.Collections;

public class LeaderBehavior : AgentBehavior {

	private Rigidbody rigidbody;
	const float MAX_ANGULAR = 25f;

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

		//Smooth rotation
		Vector3 targetPoint = currentPosition;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		velocity = currentPosition - previousPosition;
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position, transform.position + influences);
	}
}
