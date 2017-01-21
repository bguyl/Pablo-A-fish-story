using UnityEngine;

public class BoidBehavior : AgentBehavior {

	public GameObject leader;
    public EnvManager EnvInstance;
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

		//speed = (GetSpeedInfluence() + GetLeaderSpeedInfluence())/2f;
		speed = GetSpeedInfluence();

		Vector3 currentPosition = transform.position + influences * Time.deltaTime * speed;
		rigidbody.MovePosition(currentPosition);
		transform.rotation = Quaternion.LookRotation(transform.forward);

		//Smooth rotation
		Vector3 targetPoint = currentPosition;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		velocity = currentPosition - previousPosition;
	}

	void OnDrawGizmos(){
		// Gizmos.DrawLine(transform.position, transform.position + velocity);
		// Gizmos.color = Color.red;
		// Gizmos.DrawLine(transform.position, transform.position + GetLeaderInfluence());

		// Gizmos.color = Color.green;
		// Gizmos.DrawLine(transform.position, transform.position + GetSeparationInfluence());

		// Gizmos.color = Color.gray;
		// Gizmos.DrawLine(transform.position, transform.position + GetCohesionInfluence());

		// Gizmos.color = Color.blue;
		// Gizmos.DrawLine(transform.position, transform.position + GetAlignmentInfluence());
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

	public float GetLeaderSpeedInfluence(){
		return 1f/Vector3.Distance(transform.position, leader.transform.position);
	}

}
