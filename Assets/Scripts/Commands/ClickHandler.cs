using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
  public Transform spherePrefab;
    public GameObject WaveFx;
    int layerMask;
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }
    void FixedUpdate() {
        if (!GameManager.Instance.IsInit) return;
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, layerMask)) {
                if (hit.collider.tag == "Ground") {
                    GameObject obj = Instantiate(WaveFx, hit.point, Quaternion.identity) as GameObject;
                    obj.transform.Rotate(new Vector3(-90, 0, 0));
                    StartCoroutine(WaitSpawnSphere(hit.point));
                }
            }
        }
    }
    IEnumerator WaitSpawnSphere(Vector3 pos)
    {
       yield return new WaitForSeconds(1.2f);
        Transform sphere = Instantiate(spherePrefab) as Transform;
        sphere.position = pos;
    }
}
