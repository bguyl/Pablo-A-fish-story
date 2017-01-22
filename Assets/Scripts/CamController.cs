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

    //Indicator
    public RectTransform Indicator;
    public GameObject TargetFish;
    Vector3 MiddleScreen;

    Camera Cam;

    void Start()
    {

        LeaderFish = GameObject.FindGameObjectWithTag("Leader").GetComponentInChildren<SkinnedMeshRenderer>();
        Cam = GetComponent<Camera>();
        Cam.fieldOfView = minFOV;
        MiddleScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }

    void Update () {

        if (!GameManager.Instance.IsInit) return;
        Bounds bbox = LeaderFish.bounds;
        
        if (Fishes.Count > 0)
        {
            foreach (SkinnedMeshRenderer fish in Fishes)
            {
                bbox.Encapsulate(fish.bounds);
            }
        }
        

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

        if (!TargetFish || TargetFish == GameManager.Instance) return;
        //Indicator
        Vector3 TargetPosScreen = Cam.WorldToScreenPoint(TargetFish.transform.position);

        if (TargetPosScreen.x> 0 & TargetPosScreen.x< Screen.width & TargetPosScreen.y >0 & TargetPosScreen.y<Screen.height)
        {
            if (Indicator.gameObject.activeSelf) Indicator.gameObject.SetActive(false);
            return;
        }
        if (!Indicator.gameObject.activeSelf) Indicator.gameObject.SetActive(true);

        Vector3 Dir = TargetFish.transform.position - LeaderFish.transform.position;
        Dir.Normalize();

        Vector3 AbsDir = new Vector3(Mathf.Abs(Dir.x), 0, Mathf.Abs(Dir.z));

        Vector3 NewPos = MiddleScreen;
        Vector3 NewRotation = Vector3.zero;

        if (AbsDir.x > AbsDir.z)
        {
            NewPos.x = (Dir.x / AbsDir.x) * MiddleScreen.y;
            NewPos.y = Dir.z * MiddleScreen.y;
            NewRotation.z = (Mathf.Sign(NewPos.x) < 0) ? 0 : 180;
        }
        else if (AbsDir.x < AbsDir.z)
        {
            NewPos.x = Dir.x * MiddleScreen.x;
            NewPos.y = (Dir.z / AbsDir.z) * (MiddleScreen.y - 60);
            NewRotation.z = (Mathf.Sign(NewPos.y) < 0) ? 90 : -90;
        }
        else
        {
            NewPos.x = (Dir.x / AbsDir.x) * MiddleScreen.y;
            NewPos.y = (Dir.z / AbsDir.z) * (MiddleScreen.y - 60);
            NewRotation.z = (Mathf.Sign(NewPos.y) < 0) ? (Mathf.Sign(NewPos.x) < 0) ? -45 : 45 : (Mathf.Sign(NewPos.x) < 0) ? -135 : 135;
        }
        NewPos.z = Mathf.Abs(2);
        Indicator.anchoredPosition = NewPos;
        Indicator.localRotation = Quaternion.Euler(Vector3.zero);
        Indicator.localRotation = Quaternion.Euler(NewRotation);
    }


    public void AddFish(GameObject _fish)
    {
        GameManager gameInstance = GameManager.Instance;
        gameInstance.TakeByPablo(_fish);
        StartCoroutine(AddNewFish(_fish));
    }

    IEnumerator AddNewFish(GameObject _fish)
    {
        yield return new WaitForSeconds(1.0f);
        if (_fish == TargetFish || !TargetFish) TargetFish = GameManager.Instance.FindTarget();
        Fishes.Add(_fish.GetComponentInChildren<SkinnedMeshRenderer>());
    }
    public void RemoveFish(GameObject _fish){
        Fishes.Remove(_fish.GetComponentInChildren<SkinnedMeshRenderer>());
    }
}
