using UnityEngine;
using System.Collections;

public class PendingBehavior : MonoBehaviour {
  public bool pending = true;
  public Vector3 radius = new Vector3(0.1f, 0.0f, 0.0f);

  private float currentRotation;
  private Quaternion rotation;
  private Vector3 basePosition;
  private int speed;

  // Use this for initialization
  void Start () {
    currentRotation = Random.Range(0.0f, 360.0f);
    basePosition = transform.position;
    speed = Random.Range(10, 200);
  }

  // Update is called once per frame
  void Update () {
    if (pending) {
      currentRotation += Time.deltaTime * speed;
      rotation.eulerAngles = new Vector3(0, currentRotation, 0);
      transform.position += rotation * radius;
    }
  }
}
