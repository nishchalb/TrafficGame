using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour {

	public InputField verticalgreen;
	public InputField horizontalgreen;
	public InputField cycleTime;
	public InputField offset;
	public Toggle vfirst;

	private Dictionary<string, Dictionary<string, string>> settings;
	private string trafficLight; // this is determined from the click
	private Dictionary<string, InputField> mapping; 
	// add vfirst to above at some point

	// Use this for initialization
	void Start () {
		Debug.Log ("UserInput is working");
		settings = new Dictionary<string, Dictionary<string, string>> ();
		mapping = new Dictionary<string, InputField>()
		{
			{"verticalgreen", verticalgreen},
			{"horizontalgreen", horizontalgreen},
			{"cycleTime", cycleTime},
			{"offset", offset},
		};
		verticalgreen.onValueChanged.AddListener (delegate {UpdateFields("verticalgreen"); });
		horizontalgreen.onValueChanged.AddListener (delegate {UpdateFields("horizontalgreen"); });
		cycleTime.onValueChanged.AddListener (delegate {UpdateFields("cycleTime"); });
		offset.onValueChanged.AddListener (delegate {UpdateFields("offset"); });
		// do for vfirst
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(
				Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				if (trafficLight == null) {
					trafficLight = hit.transform.gameObject.name;
				}
				else if (trafficLight != hit.transform.gameObject.name) {
					ResetInputs ();
					if (settings.ContainsKey (trafficLight)) {
						FillInputs ();
					}
					trafficLight = hit.transform.gameObject.name;
				}
			}
		}
	}
	void ResetInputs () {
		verticalgreen.text = "";
		horizontalgreen.text = "";
		cycleTime.text = "";
		offset.text = "";
	}

	void FillInputs () {
		// fill inputs with appropriate vals
		foreach (var item in settings[trafficLight]) {
			mapping[item.Key].text = item.Value;
		}

	}

	public void UpdateFields(string param) {
		// if trafficLight null, ask which traffic light?
		if (trafficLight != null) {
			if (!settings.ContainsKey (trafficLight))
			settings [trafficLight] [param] = mapping[param].text;
		}
	}
}
