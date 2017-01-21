using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
  public Transform spherePrefab;
  public LayerMask myLayerMask;

  void Update () {
    if (Input.GetMouseButtonDown(0)) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,myLayerMask)) {
                Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Ground") {
                Transform sphere = Instantiate(spherePrefab) as Transform;
                sphere.position = hit.point;
            }
        }
    }
  }
}
