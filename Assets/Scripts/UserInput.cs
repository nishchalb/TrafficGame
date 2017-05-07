using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour {
	public GameObject light;
	public InputField verticalgreen;
	public InputField horizontalgreen;
	public InputField cycleTime;
	public InputField offset;
	public Toggle vfirst;

	private Dictionary<string, Dictionary<string, int>> settings;

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
			if (Physics.Raycast(ray, out hit, 100)) {
				Debug.Log(
					hit.transform.gameObject.name );
			}
		}
	}
}
