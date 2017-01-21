using UnityEngine;

public class BoidBehavior : AgentBehavior {

	public GameObject leader;
	private LeaderBehavior leaderBehavior;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        leader = GameObject.FindGameObjectWithTag("Leader");
		rigidbody = GetComponent<Rigidbody>();
		leaderBehavior = leader.GetComponent<LeaderBehavior>();
		influences = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 previousPosition = transform.position;
		influences = new Vector3(0,0,0);
        
		//Apply boid behavior
		influences += GetAlignmentInfluence();
		influences += GetCohesionInfluence();
		influences += GetSeparationInfluence();
		influences += GetLeaderInfluence();

		Vector3 currentPosition = transform.position + influences * Time.deltaTime * speed;
		rigidbody.MovePosition(currentPosition);
		transform.rotation = Quaternion.LookRotation(transform.forward);
		velocity = currentPosition - previousPosition;
		Debug.Log("Boid: "+velocity);
	}

	void OnDrawGizmos(){
		//Gizmos.DrawLine(transform.position, transform.position + velocity);
		Gizmos.DrawLine(transform.position, transform.position + GetLeaderInfluence());
	}

    public Vector3 GetLeaderInfluence(){
        //TODO: Rename constant
        int cst = 5;
        Vector3 result = Vector3.zero;
        result = - leaderBehavior.Velocity;
        result = Vector3.Normalize(result) * cst;
        result += leader.transform.position;
		return (result - transform.position);
    }

}
