using UnityEngine;
using System.Collections;

public class BoidBehavior : MonoBehaviour {

	Agent agent; 
	Vector3 destination;

	CharacterController ctrl;

	// Use this for initialization
	void Start () {
		agent = new Agent(transform.position);
		destination = new Vector3(50, Random.Range(0.0f,100.0f), Random.Range(0.0f,100.0f));
		ctrl = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(gameObject.tag == "leader"){
			ControlHandler();
			return;
		}

		agent.Position = transform.position;
		agent.Velocity = Vector3.MoveTowards(transform.position, destination, 0.125f) - transform.position;
		transform.position = Vector3.MoveTowards(transform.position, destination, 0.125f);
		destination += agent.GetAlignment();
		destination += agent.GetCohesion();
		destination += agent.GetSeparation();
		destination.x = 50;
	}

	void ControlHandler() {
		Vector3 before = destination;

		float horizontal = -Input.GetAxis("Horizontal")*10;
		float vertical = Input.GetAxis("Vertical")*10;
		destination = new Vector3(50, vertical, horizontal);
		destination = transform.TransformDirection(destination);

		agent.Velocity = destination - before;

		if(ctrl)
			ctrl.Move(destination * Time.deltaTime);
	}

	void OnTriggerEnter(Collider neighbor) {
		BoidBehavior neighborBehavior = neighbor.GetComponentInParent(typeof(BoidBehavior)) as BoidBehavior;
		this.agent.Neighbors.Add(neighborBehavior.agent);
	}

	void OnTriggerExit(Collider neighbor){
		BoidBehavior neighborBehavior = neighbor.GetComponentInParent(typeof(BoidBehavior)) as BoidBehavior;
		this.agent.Neighbors.Remove(neighborBehavior.agent);
	}
}
