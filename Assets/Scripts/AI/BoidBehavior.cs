using UnityEngine;

public class BoidBehavior : AgentBehavior {

	public GameObject leader;
    public GameManager GameInstance;
	private LeaderBehavior leaderBehavior;
    public PendingBehavior pendingScript;
	private Rigidbody rigidbody;
	private Camera camera;

	private float max_speed;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		influences = transform.position;
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		influences = new Vector3(0,0,0);
        
		//Apply boid behavior
		influences += GetAlignmentInfluence();
		influences += GetCohesionInfluence();
		influences += GetSeparationInfluence();
		influences += GetLeaderInfluence();
		influences = Vector3.Normalize(influences.normalized);
		influences = new Vector3(influences.x, 0, influences.z);
		float desiredSpeed = speed * GetLeaderSpeedInfluence();
		velocity = influences * Time.deltaTime * desiredSpeed;
		
		//Smooth rotation + Move
		Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
		rigidbody.MovePosition(transform.position + velocity);
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position, transform.position + influences);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + GetAlignmentInfluence());
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + GetCohesionInfluence());
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + GetSeparationInfluence());
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.position + GetLeaderInfluence());
	}

	protected override void OnTriggerEnter(Collider c){
		base.OnTriggerEnter(c);
        if (c.gameObject.layer == 10 & !leaderBehavior)
        {
            bool IsShoal = false;
            if (c.tag == "Leader")
            {
                leaderBehavior = c.GetComponent<LeaderBehavior>();
                IsShoal = true;
            }
            else if (c.tag == "Fish")
            {
                BoidBehavior script = GetComponent<BoidBehavior>();
                if (script.leaderBehavior)
                {
                    leaderBehavior = script.leaderBehavior;
                    IsShoal = true;
                }
            }if (IsShoal)
            {
                camera.GetComponent<CamController>().AddFish(gameObject);
                pendingScript.pending = false;
            }
        }
	}

	public Vector3 GetLeaderInfluence(){
		if(!leaderBehavior)
			return new Vector3(0,0,0);
		//TODO: Rename constant
		float cst = 2f;
		Vector3 result = Vector3.zero;
		result = - leaderBehavior.Velocity;
		result = Vector3.Normalize(result) * cst;
		result += leaderBehavior.transform.position;
			return (result - transform.position);
	}

	public float GetLeaderSpeedInfluence(){
		if(leaderBehavior == null)
			return 1;
		max_speed = leaderBehavior.speed * 1.5f;
		float distance = Vector3.Distance(transform.position, leaderBehavior.transform.position);
		//TODO: Remove constant (7 is the minimal distance)
		if(distance > 7){
			if( (distance * speed) < max_speed){
				return distance;
			}
			else{
				float ratio = (max_speed/speed);
				return ratio;
			}
		}
		return (distance/7f);
	}

}
