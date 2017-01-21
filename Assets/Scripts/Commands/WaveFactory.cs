﻿using UnityEngine;
using System.Collections;

public class WaveFactory : MonoBehaviour {
  public int startSize = 1;
  private int maxSize;
  private float speed;

  private Vector3 targetScale;
  private Vector3 baseScale;

  // Use this for initialization
  void Start () {
    maxSize = 25;
    speed = 1.4f;
    baseScale = transform.localScale;
    transform.localScale = baseScale * startSize;
    targetScale = baseScale * maxSize;
  }

  // Update is called once per frame
  void Update () {
    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
    if (transform.localScale.x >= 17.9f) {
      Destroy(gameObject);
    }
  }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
    }

  void OnTriggerEnter(Collider c) {
    
        if (c.tag == "Leader")
        {
            var force = 50f;
            var dir = transform.position - c.gameObject.transform.position;
            dir = -dir.normalized;
            dir.z = 0f;
            c.GetComponent<LeaderBehavior>().SetWaveInfluence(dir);
        }
    
    }
}
