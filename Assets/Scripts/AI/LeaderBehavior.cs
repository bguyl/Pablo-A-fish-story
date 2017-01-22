using UnityEngine;
using System.Collections;

public class LeaderBehavior : AgentBehavior {

	private Rigidbody rigidbody;
    public GameManager GameInstance;
	const float MAX_ANGULAR = 25f;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!GameManager.Instance.IsInit) return;
		influences = transform.forward;
		influences += GetWaveInfluence();
		influences = Vector3.Normalize(influences);
		influences = new Vector3(influences.x, 0, influences.z);
		
		velocity = influences * Time.deltaTime * speed;

		//Smooth rotation + Move
		Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
		rigidbody.MovePosition(transform.position + velocity);

	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position, transform.position + influences);
	}
}
