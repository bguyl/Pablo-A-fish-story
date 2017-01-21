using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
  public Transform spherePrefab;
    int layerMask;
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }
  void FixedUpdate () {
    if (Input.GetMouseButtonDown(0)) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,100.0f, layerMask)) {
                Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Ground") {
                Transform sphere = Instantiate(spherePrefab) as Transform;
                sphere.position = hit.point;
            }
        }
    }
  }
}
