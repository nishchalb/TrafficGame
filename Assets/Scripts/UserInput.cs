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
	public Text title;

	private Dictionary<string, Dictionary<string, string>> settings;
	public string trafficLight; // this is determined from the click
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
        vfirst.onValueChanged.AddListener(delegate { UpdateFields("vfirst"); });
		// do for vfirst
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(
				Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
                trafficLight = hit.transform.gameObject.name;
				title.text = hit.transform.gameObject.name;
                if (settings.ContainsKey(trafficLight))
                {
                    FillInputs();
                } else
                {
                    ResetInputs();
                }


			}
		}
	}
	void ResetInputs () {
		Debug.Log ("reset inputs is called");
		verticalgreen.text = "";
		horizontalgreen.text = "";
		cycleTime.text = "";
		offset.text = "";
        vfirst.isOn = true;
	}

	void FillInputs () {
		// fill inputs with appropriate vals
		foreach (string key in mapping.Keys) {
			Debug.Log ("item is " + key + " " + settings[trafficLight][key]);
			mapping[key].text = settings[trafficLight][key];
		}
        vfirst.isOn = System.Convert.ToBoolean(settings[trafficLight]["vfirst"]);

	}

	public void UpdateFields(string param) {
		// if trafficLight null, ask which traffic light?
		if (trafficLight != null) {
			if (!settings.ContainsKey (trafficLight)) {
				settings [trafficLight] = new Dictionary<string, string>()
				{
					{"verticalgreen", ""},
					{"horizontalgreen", ""},
					{"cycleTime", ""},
					{"offset", ""},
					{"vfirst", "true"}
				};
			}
            if (param == "vfirst")
            {
                if (vfirst.isOn) settings[trafficLight][param] = "true";
                else if (!vfirst.isOn) settings[trafficLight][param] = "false";

            } else
            {
                settings[trafficLight][param] = mapping[param].text;
            }
			
			Debug.Log ("inserting into dic " + param + " " + settings[trafficLight][param]);
		}
	}

    public Dictionary<string, string> GetSavedSettingsForLight(string traffic)
    {
		if (settings.ContainsKey (traffic)) {
			return settings [traffic];
		} else {
			return new Dictionary<string, string> ();
		}
        
    }
}
