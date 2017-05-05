using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("ClickDetection is working");	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(
				Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				Debug.Log(
					hit.transform.gameObject.name );
			}
		}
	}
}
