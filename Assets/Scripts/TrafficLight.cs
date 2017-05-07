using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : MonoBehaviour {
	public string vertical;
	public string horizontal;
	public InputField verticalgreen;
	public InputField horizontalgreen;
	public InputField cycleTime;
	public InputField offset;
	public Toggle vfirst;
	public Text hlabel; //temp
	public Text vlabel; //temp

	private int ctime;
	private float vgreen;
	private float vred;
	private float hred;
	private float hgreen;
	private float time;

	// Use this for initialization
	void Start () {
		vertical = "red";
		horizontal = "red";
		time = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (cycleTime.text.Length > 0) {

			ctime = System.Int32.Parse (cycleTime.text);
			time = (time + Time.deltaTime) % ctime;
			if (verticalgreen.text.Length > 0) {
				vgreen = ctime * System.Int32.Parse (verticalgreen.text) / 100.0F;
				vred = ctime * (1 - System.Int32.Parse (verticalgreen.text) / 100.0F);
			} else {
				vgreen = 0;
				vred = 0;
			} 
			if (horizontalgreen.text.Length > 0) {
				hgreen = ctime * System.Int32.Parse (horizontalgreen.text) / 100.0F;
				hred = ctime * (1 - System.Int32.Parse (horizontalgreen.text) / 100.0F);
			} else {
				hgreen = 0;
				hred = 0;
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

			hlabel.text = "h: " + horizontal;
			vlabel.text = "v: " + vertical;
		}
	}
}
