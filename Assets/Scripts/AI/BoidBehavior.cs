using UnityEngine;

public class BoidBehavior : AgentBehavior {

	public GameObject leader;
    public GameManager GameInstance;
	private LeaderBehavior leaderBehavior;
    public PendingBehavior pendingScript;
	private Rigidbody rigidbody;
	private Camera camera;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		influences = transform.position;
		camera = Camera.main;
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
		//speed *= GetSpeedInfluence();

		Vector3 currentPosition = transform.position + influences * Time.deltaTime * speed;
			rigidbody.MovePosition(currentPosition);
		transform.rotation = Quaternion.LookRotation(transform.forward);

		//Smooth rotation
		Vector3 targetPoint = currentPosition;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		velocity = currentPosition - previousPosition;
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
		int cst = 3;
		Vector3 result = Vector3.zero;
		result = - leaderBehavior.Velocity;
		result = Vector3.Normalize(result) * cst;
		result += leaderBehavior.transform.position;
			return (result - transform.position);
	}

	public float GetLeaderSpeedInfluence(){
		float distance = Vector3.Distance(transform.position, leaderBehavior.transform.position);
		if(distance != 0 && leaderBehavior != null)
			return (1f/Vector3.Distance(transform.position, leaderBehavior.transform.position));
		return 1;
	}

}
