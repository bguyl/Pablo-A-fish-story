using System.Collections.Generic;
using UnityEngine;

class Agent
{
    List<Agent> neighbors;
    private Vector3 position;
    private Vector3 velocity;

    public Agent(Vector3 position){
        this.position = position;
        this.velocity = new Vector3(0, 0, 0);
        this.neighbors = new List<Agent>();
    }

    public List<Agent> Neighbors {
        get { return neighbors; }
        set { this.neighbors.AddRange(value); }
    }

    public Vector3 Position {
        get { return position; }
        set { this.position = value; }  
    }

    public Vector3 Velocity {
        get { return velocity; }
        set { this.velocity = value; }
    }

    public Vector3 GetSeparation(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        foreach (Agent neighbor in neighbors)
        {
            //Inverse Distance Squared
            float ids = (1/(Vector3.Distance(this.Position, neighbor.Position)));
            //Reverse direction vector
            Vector3 currentDirection = this.Position - neighbor.Position;
            //currentDirection = Vector3.Normalize(currentDirection);
            //Scale by 'gravity formula'
            Vector3 scaled = Vector3.Scale(currentDirection, new Vector3(ids, ids, ids));
            result += scaled;
        }
        return result;
    }

    public Vector3 GetAlignment(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        //Averagin neighbors' velocities
        foreach(Agent neighbor in neighbors){
            result += neighbor.Velocity;
        }
        float inverseCount = (1/neighbors.Count);
        result.Scale(new Vector3(inverseCount, inverseCount, inverseCount));

        return result;
    }

    public Vector3 GetCohesion(){
        Vector3 result = new Vector3(0,0,0);
        if(neighbors.Count == 0)
            return result;

        //Averagin neighbors' positions
        foreach(Agent neighbor in neighbors){
            result += neighbor.Position;
        }
        float inverseCount = (1/neighbors.Count);
        result.Scale(new Vector3(inverseCount, inverseCount, inverseCount));
        
        return result;
    }

}