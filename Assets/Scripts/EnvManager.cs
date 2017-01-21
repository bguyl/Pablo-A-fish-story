using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvManager : MonoBehaviour {

    public int ID = 0;
    public GameManager Gameinstance;
    public GameObject PrefabBoid;

    Transform[] SpawnPoints;
    List<GameObject> FishesSpawn = new List<GameObject>();
	// Use this for initialization

    void Start()
    {
        Transform spawnRoot = transform.FindChild("SpawnPoints");
        SpawnPoints = new Transform[spawnRoot.childCount];

        for (int n = 0; n < SpawnPoints.Length; n++)
        {
            SpawnPoints[n] = spawnRoot.GetChild(n);
        }
    }
	public void Init () {

        if (FishesSpawn.Count > 0 || Gameinstance.FishCanSpawn == 0) return;
        int MaxSpawn = Mathf.Min(Gameinstance.FishCanSpawn, SpawnPoints.Length);
        for (int i =0; i< MaxSpawn; i++)
        {
            GameObject obj = Instantiate(PrefabBoid, SpawnPoints[i].position, Quaternion.identity) as GameObject;
            FishesSpawn.Add(obj);
            obj.GetComponent<BoidBehavior>().EnvInstance = this;
        }
	}
	
    public void RemoveFish(GameObject _fish)
    {
        FishesSpawn.Remove(_fish);
        FishesSpawn.Sort();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
