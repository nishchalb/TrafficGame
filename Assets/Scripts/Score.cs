using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public string score;

	public float variance;
	public float mean;

	private float[] waits;
	private GameObject[] cars; // make into list for variance
	private int i;

	// Use this for initialization
	void Start () {
		mean = 0;
		variance = 0;
		cars = GameObject.FindGameObjectsWithTag ("Car");
		
	}
	
	// Update is called once per frame
	void Update () {
		waits = new float[cars.Length];
		i = 0;
		foreach (GameObject car in cars) {
			waits [i] = 0; // should be equal to car's waittime
		}

	}
}
