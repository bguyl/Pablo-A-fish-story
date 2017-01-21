using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    GameObject LeaderFish;
    public GameObject MainMenuPanel;
    public GameObject[] EnvPart;

    int lastPart = 0;
    public int FishCanSpawn = 20;
	// Use this for initialization
	void Start () {

        LeaderFish = GameObject.FindGameObjectWithTag("Leader");
        for (int i = 0; i < EnvPart.Length; i++)
        {
            EnvManager _env = EnvPart[i].GetComponent<EnvManager>();
            _env.ID = i;
            _env.Gameinstance = this;
        }

        ChangeEnvPart(0);
	}
	
    public void ChangeEnvPart( int _part)
    {
        EnvPart[_part].SetActive(true);
        EnvPart[_part].GetComponent<EnvManager>().Init();
        lastPart = _part;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickPlay()
    {
        MainMenuPanel.SetActive(false);
    }
}
