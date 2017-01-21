﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IntroFish : MonoBehaviour {
    public float FishSpeed;
    List<GameObject> Fishes = new List<GameObject>();
    bool CanRotate = true;
    Vector3 NewRot;
    float StartTime = 0;
    public float TimeBetweenChangeRotate;
	// Use this for initialization
	void Start () {
        StartTime = Time.time;

        FishSpeed = Random.Range(0.5f, 2.0f);
        for (int i =0; i<transform.childCount; i++)
        {
            Fishes.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float d = (Time.time - StartTime) / TimeBetweenChangeRotate;
        foreach( GameObject _fish in Fishes)
        {
            RectTransform tr = _fish.GetComponent<RectTransform>();
            Vector3 PosFish = tr.anchoredPosition;
            if (d>= 1.0f)
            {
                StartTime = Time.time;
                NewRot = new Vector3(0, 0, Random.Range(-5, 5)*Time.deltaTime);
                TimeBetweenChangeRotate = Random.Range(3, 6);
            }
            //Debug.Log((Screen.width + PosFish.x) % Screen.width);
            //PosFish = new Vector3(((Screen.width*2 + PosFish.x + (Screen.width/2.0f)) % Screen.width*2)- (Screen.width), ((Screen.height + PosFish.y + (Screen.height / 2.0f)) % Screen.height)-(Screen.height/2.0f), 0);
           
            tr.anchoredPosition = PosFish;
            if (PosFish.y > 300)
            {
                PosFish.y = -300;
            }
            else if (PosFish.y < -300)
            {
                PosFish.y = 300;
            }
            PosFish.y = (PosFish.y > 300) ? -300 : (PosFish.y < -300) ? 300 : PosFish.y;
            PosFish.x = (PosFish.x > 600)? - 600 : (PosFish.x < -600)? 600  : PosFish.x;

            tr.anchoredPosition = PosFish;


            _fish.transform.Rotate(NewRot);
            _fish.transform.Translate(transform.right * FishSpeed * Time.deltaTime);
        }
         

	}
}