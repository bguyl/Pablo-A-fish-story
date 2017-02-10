using UnityEngine;
using System.Collections;

public class SpawnFish : MonoBehaviour {
    public GameObject[] FishPrefabs;
    public Texture SkinFish2;

	// Use this for initialization
	void Start () {
        int _prefabIndex = Random.Range(0, FishPrefabs.Length - 1);
        GameObject obj = Instantiate(FishPrefabs[_prefabIndex], transform.position, Quaternion.identity) as GameObject;

        int i = Random.Range(0, 1);
        if(i > 0)
        {
            obj.GetComponentInChildren<Renderer>().material.mainTexture = SkinFish2;
        }
        
        obj.transform.SetParent(transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.Rotate(new Vector3(0, -90, 0));
        

	}
	

}
