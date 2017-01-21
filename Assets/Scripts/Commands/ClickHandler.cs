using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
  public Transform spherePrefab;

  void Update () {
    if (Input.GetButtonDown("Fire1")) {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.point.z >= transform.position.z - 0.001f) {
          var sphere = Instantiate(spherePrefab) as Transform;
          sphere.position = hit.point;
        }
      }
    }
  }
}
