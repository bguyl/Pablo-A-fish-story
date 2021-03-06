﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    GameObject LeaderFish;
    public GameObject MainMenuPanel;
    public GameObject CreditPanel;
    public GameObject EndGamePanel;
    public GameObject Environment;
    public GameObject HUD;

    public bool IsInit = false;
    int lastPart = 0;
    public int FishCanSpawn = 20;
    int fishTake = 0;
    public static GameManager Instance;

    //spawn boid
    public GameObject PrefabBoid;

    Transform[] SpawnPoints;
    List<GameObject> FishesSpawn = new List<GameObject>();
    Text ScoreTxt;
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {

        LeaderFish = GameObject.FindGameObjectWithTag("Leader");
        ScoreTxt = HUD.transform.GetChild(0).GetComponent<Text>();
        Transform spawnRoot = Environment.transform.FindChild("SpawnPoints");
        SpawnPoints = new Transform[spawnRoot.childCount];

        for (int n = 0; n < SpawnPoints.Length; n++)
        {
            SpawnPoints[n] = spawnRoot.GetChild(n);
        }

        ScoreTxt.text = FishCanSpawn.ToString();
        Spawn();

    }

    void Spawn()
    {
        if (FishCanSpawn == 0) return;
        int MaxSpawn;
        
        MaxSpawn = (FishesSpawn.Count > 0) ? 1 : Random.Range(1, SpawnPoints.Length);
        for (int i = 0; i < MaxSpawn; i++)
        {
            int _indexSpawn = Random.Range(0, SpawnPoints.Length - 1);
            GameObject obj = Instantiate(PrefabBoid, SpawnPoints[_indexSpawn].position, Quaternion.identity) as GameObject;
            FishesSpawn.Add(obj);
            FishCanSpawn--;
            obj.GetComponent<BoidBehavior>().GameInstance = this;
        }
    }


    public GameObject FindTarget()
    {
        GameObject closestFish = gameObject;
        float DistanceMax = Mathf.Infinity;
        Vector3 posLeader = LeaderFish.transform.position;

        foreach (GameObject _fish in FishesSpawn)
        {
            Vector3 d = _fish.transform.position - posLeader;
            float distance = d.sqrMagnitude;
            if (distance < DistanceMax)
            {
                closestFish = _fish;
                DistanceMax = distance;
            }

        }
        return closestFish;
    }

    public void TakeByPablo(GameObject _fish)
    {
        FishesSpawn.Remove(_fish);
        ScoreTxt.text =  (int.Parse(ScoreTxt.text) -1).ToString();
        Spawn();
        if (FishesSpawn.Count == 0 & FishCanSpawn == 0) EndGame();
    }


    //UI
    void ChangePanel(GameObject _panel)
    {
        _panel.SetActive(true);
    }
    public void OnClickPlay()
    {
        MainMenuPanel.SetActive(false);
        IsInit = true;
    }

    public void OnClickCredit()
    {
        ChangePanel(CreditPanel);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    void EndGame()
    {
        HUD.SetActive(false);
        ChangePanel(EndGamePanel);
    }

}
