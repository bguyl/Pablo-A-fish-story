using UnityEngine;

public class BoidBehavior : AgentBehavior {

	public GameObject leader;
	LeaderBehavior leaderBehavior;

	// Use this for initialization
	void Start () {
        leader = GameObject.FindGameObjectWithTag("Leader");
		leaderBehavior = leader.GetComponent<LeaderBehavior>();
		destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//Apply boid behavior
		destination += GetAlignmentInfluence();
		destination += GetCohesionInfluence();
		destination += GetSeparationInfluence();
		destination += GetLeaderInfluence();
	
		velocity = Vector3.MoveTowards(transform.position, destination, 0.125f) - transform.position;
		transform.position = Vector3.MoveTowards(transform.position, destination, 0.125f);
		transform.rotation = Quaternion.LookRotation(velocity);
	}


    public Vector3 GetLeaderInfluence(){
        //TODO: Rename constant
        int cst = 0;
        Vector3 result;
        result = - leaderBehavior.Velocity;
        result = Vector3.Normalize(result) * cst;
        result += leader.transform.position;
		return (result - transform.position);
    }

}
