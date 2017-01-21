using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    GameObject LeaderFish;
    public GameObject MainMenuPanel;
	// Use this for initialization
	void Start () {

        LeaderFish = GameObject.FindGameObjectWithTag("Leader");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickPlay()
    {
        MainMenuPanel.SetActive(false);
    }
}
