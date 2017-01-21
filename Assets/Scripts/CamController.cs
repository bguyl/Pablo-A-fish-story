using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamController : MonoBehaviour {

    SkinnedMeshRenderer LeaderFish;
    List<SkinnedMeshRenderer> Fishes = new List<SkinnedMeshRenderer>();
    GameObject[] FishesTest;

    public Collider MapBouds;

    [Header("CamInfo")]
    public float minFOV = 20;
    public float maxFOV = 120;


    Camera Cam;

    void Start()
    {

        LeaderFish = GameObject.FindGameObjectWithTag("Leader").GetComponentInChildren<SkinnedMeshRenderer>();
        Cam = GetComponent<Camera>();
        Cam.fieldOfView = minFOV;

        //ForceAspectRatio

        float TargetAspect = 16.0f / 10.0f;

        // current aspect ratio
        float CurrentAspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = CurrentAspect / TargetAspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = Cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            Cam.rect = rect;
        }
        else 
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = Cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            Cam.rect = rect;
        }
    }
	

	void Update () {

        //Test
        // FishesTest = GameObject.FindGameObjectsWithTag("Fish");

        // foreach(GameObject _fish in FishesTest)
        // {
        //     Fishes.Add(_fish.GetComponentInChildren<SkinnedMeshRenderer>());
        // }
        //EndTest

        //Bounds MapBbox = MapBouds.bounds;
        Bounds bbox = LeaderFish.bounds;
        
        if (Fishes.Count > 0)
        {
            foreach (SkinnedMeshRenderer fish in Fishes)
            {
                bbox.Encapsulate(fish.bounds);
            }
        }
        

        //bbox.Expand(new Vector3(5, 0, 5));

        //FOV
        float frustrumHeight = Mathf.Max((bbox.max.z - bbox.min.z), (bbox.max.x - bbox.min.x)/Cam.aspect);
        frustrumHeight *= 2;
        float NewFOV = 2 * Mathf.Atan(frustrumHeight * 0.5f / transform.position.y) * Mathf.Rad2Deg;

        if (NewFOV > minFOV || NewFOV < maxFOV)
        {
            float IndexFov = (NewFOV - minFOV) / (maxFOV - minFOV);
            Cam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, IndexFov);
        } 

        

        //CamPosition
        Vector3 NewCamPos;

        NewCamPos = LeaderFish.transform.position;
         
        NewCamPos.y = transform.position.y;

        transform.position = NewCamPos;
    }


    public void AddFish(GameObject _fish)
    {
        Fishes.Add(_fish.GetComponentInChildren<SkinnedMeshRenderer>());
    }

    public void RemoveFish(GameObject _fish){
        Fishes.Remove(_fish.GetComponentInChildren<SkinnedMeshRenderer>());
    }
}
