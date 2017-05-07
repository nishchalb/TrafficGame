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
	private string trafficLight;

	// Use this for initialization
	void Start () {
		Debug.Log ("UserInput is working");
		settings = new Dictionary<string, Dictionary<string, int>> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("something was clicked!");
			Ray ray = Camera.main.ScreenPointToRay(
				Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				trafficLight = hit.transform.gameObject.name;
				if (settings.ContainsKey (trafficLight)) {
					
				} else {
					ResetInputs ();
					if (cycleTime.text.Length > 0) {
						
					}

				}
			}
		}
	}
	void ResetInputs () {
		// reset all inputs
	}

	void FillInputs () {
		// fill inputs with appropriate vals
	}

	void UpdateSettings(string objectName, string parameter, int value) {
		// either add or update object
		if (settings.ContainsKey (trafficLight)) {
			settings [trafficLight] [parameter] = value;
		} else {
			settings [trafficLight] = new Dictionary<string, int> ();
			UpdateSettings (objectName, parameter, value);
		}
	}
}
