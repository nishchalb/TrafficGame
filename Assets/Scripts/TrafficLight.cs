using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : MonoBehaviour {
	public string vertical;
	public string horizontal;

    public UserInput userInput;

    private LightChange[] lights;

    private int ctime;
	private float vgreen;
	private float vred;
	private float hred;
	private float hgreen;
	private float time;
    private bool vfirst;
    private Dictionary<string, InputField> mapping;

    // Use this for initialization
    void Start () {
		vertical = "red";
		horizontal = "red";
		time = 0;
        lights = GetComponentsInChildren<LightChange>();
        vfirst = true;
    }
	
	// Update is called once per frame
	void Update () {
        Dictionary<string, string> settings = userInput.GetSavedSettingsForLight(gameObject.name);
		if (settings.ContainsKey("cycleTime") && settings["cycleTime"].Length > 0) {

			ctime = System.Int32.Parse (settings["cycleTime"]);
			time = (time + Time.deltaTime) % ctime;
			if (settings.ContainsKey("verticalgreen") && settings["verticalgreen"].Length > 0) {
				vgreen = ctime * System.Int32.Parse (settings["verticalgreen"]) / 100.0F;
				vred = ctime * (1 - System.Int32.Parse (settings["verticalgreen"]) / 100.0F);
			} else {
				vgreen = 0;
				vred = 0;
			} 
			if (settings.ContainsKey("horizontalgreen") && settings["horizontalgreen"].Length > 0) {
				hgreen = ctime * System.Int32.Parse (settings["horizontalgreen"]) / 100.0F;
				hred = ctime * (1 - System.Int32.Parse (settings["horizontalgreen"]) / 100.0F);
			} else {
				hgreen = 0;
				hred = 0;
			}
            if (settings.ContainsKey("vfirst"))
            {
                vfirst = System.Convert.ToBoolean(settings["vfirst"]);
            }

            if (vfirst) {
				if (time <= vgreen) {
					vertical = "green";
				} else {
					vertical = "red";
				}
				if (time <= hred) {
					horizontal = "red";
				} else {
					horizontal = "green";
				}
			} else {
				if (time <= vred) {
					vertical = "red";
				} else {
					vertical = "green";
				}
				if (time <= hgreen) {
					horizontal = "green";
				} else {
					horizontal = "red";
				}
			}
		}

        foreach (LightChange lc in lights)
        {
            if (lc.gameObject.CompareTag("HorizontalLight"))
            {
                lc.setLight(horizontal);
            } else if (lc.gameObject.CompareTag("VerticalLight"))
            {
                lc.setLight(vertical);
            }
        }

    }

    private void FixedUpdate()
    {
    }
}
