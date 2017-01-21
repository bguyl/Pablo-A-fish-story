using UnityEngine;
using System.Collections;

public class LeaderBehavior : AgentBehavior {

	// Use this for initialization
	void Start () {
		destination = new Vector3(0,50,0);
	}
	
	// Update is called once per frame
	void Update () {
		velocity = Vector3.MoveTowards(transform.position, destination, 0.125f) - transform.position;
		transform.position = Vector3.MoveTowards(transform.position, destination, 0.125f);
	}
}
