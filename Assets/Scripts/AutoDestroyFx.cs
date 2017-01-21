using UnityEngine;
using System.Collections;

public class AutoDestroyFx : MonoBehaviour {

    public int LifeTime = 2;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitDead());
	}
	
	IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
