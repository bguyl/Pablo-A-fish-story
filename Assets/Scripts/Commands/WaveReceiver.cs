using UnityEngine;
using System.Collections;

public class WaveReceiver : MonoBehaviour {
  public void modifyTrajectory (Vector3 dir) {
    dir.z = 0f;
    gameObject.GetComponent<AgentBehavior>().influences += dir;
  }
}
