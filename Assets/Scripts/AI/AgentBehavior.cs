using UnityEngine;
using System.Collections.Generic;

public abstract class AgentBehavior : MonoBehaviour {
	protected Vector3 influences;
	const float Y_FACTOR_REDUCER = 0.1f; 
	protected List<AgentBehavior> neighbors = new List<AgentBehavior>();
    protected Vector3 velocity = new Vector3(0,0,0);

    public float speed = 5.1f;

    protected Vector3 waveInfluence = new Vector3(0,0,0);

    public Vector3 Velocity {
        get { return velocity; }
    }

	virtual protected void OnTriggerEnter(Collider c) {
		if(c.tag == "Fish" || c.tag == "Leader"){
			AgentBehavior neighborBehavior = c.GetComponent<AgentBehavior>();
			if(!this.neighbors.Contains(neighborBehavior))
				this.neighbors.Add(neighborBehavior);
		}
	}

	virtual protected void OnTriggerExit(Collider c){
		if(c.tag == "Fish" || c.tag == "Leader"){
			AgentBehavior neighborBehavior = c.GetComponent<AgentBehavior>();
			this.neighbors.Remove(neighborBehavior);
		}
	}

	public Vector3 GetSeparationInfluence(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        foreach (AgentBehavior neighbor in neighbors)
        {
            //Inverse Distance Squared
            float ids = (1/(Vector3.Distance(transform.position, neighbor.transform.position)));
            //Reverse direction vector
            Vector3 currentDirection = transform.position - neighbor.transform.position;
            //currentDirection = Vector3.Normalize(currentDirection);
            //Scale by 'gravity formula'
            result += currentDirection * ids;
        }
		result.y *= Y_FACTOR_REDUCER;
        return result;
    }

    public Vector3 GetAlignmentInfluence(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        //Averagin neighbors' velocities
        foreach(AgentBehavior neighbor in neighbors){
            result += neighbor.velocity;
        }
        float inverseCount = (1/neighbors.Count);
        //result.Scale(new Vector3(inverseCount, inverseCount, inverseCount));

        result *= inverseCount;
		result.y *= Y_FACTOR_REDUCER;
        return result;
    }

    public Vector3 GetCohesionInfluence(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        //Averagin neighbors' positions
        foreach(AgentBehavior neighbor in neighbors){
            result += neighbor.transform.position;
        }
        result += transform.position;
        float inverseCount = (1.0f/(neighbors.Count+1.0f));
        result = result * inverseCount;
		result = (result - transform.position);
		result.y *= Y_FACTOR_REDUCER;
        return result;

    }

    public float GetSpeedInfluence(){
        float result = 0f;
        foreach(AgentBehavior neighbor in neighbors){
            result += neighbor.speed;
        }
        result = result / neighbors.Count;

        return result;
    }

    public void SetWaveInfluence(Vector3 force) {
        force *= 3;
        waveInfluence = force;
    }

    public Vector3 GetWaveInfluence(){
        return waveInfluence;
    }
}
