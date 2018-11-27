using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userControll : MonoBehaviour {

	void Update () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.tag == "Enemy") {
						hit.transform.GetComponent<figureScript> ().Die ();
						GetComponent<UIController> ().AddScore ();
					}
				}
			}
		}
	}
}
