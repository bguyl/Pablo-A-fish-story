using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

  public Transform GameManager;
  public Transform MainCamera;

  private int totalScore;
  private int currentScore;
  private Text text;

  // Use this for initialization
  void Start () {
    text = GetComponentsInChildren<Text>()[0];
  }

  // Update is called once per frame
  void Update () {
    
  }
}
