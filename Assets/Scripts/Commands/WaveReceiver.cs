using UnityEngine;
using System.Collections;

public class WaveReceiver : MonoBehaviour {
  public void modifyTrajectory (Vector3 dir) {
    gameObject.GetComponent<AgentBehavior>().destination += dir;
  }
}
