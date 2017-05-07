using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {
	public GameObject light;

	private Hashtable settings;

	// Use this for initialization
	void Start () {
		Debug.Log ("UserInput is working");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("something was clicked!");
			Ray ray = Camera.main.ScreenPointToRay(
				Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000)) {
				Debug.Log("and I know what!");
				Debug.Log(
					hit.transform.gameObject.name );
			}
		}
	}
}
