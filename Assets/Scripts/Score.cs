using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public string score;

	public float variance;
	public float mean;
	public int vFactor;
	public int mFactor;

	private float[] waits;
	private GameObject[] cars; // make into list for variance

	// Use this for initialization
	void Start () {
		mean = 0;
		variance = 0;
		vFactor = 10;
		mFactor = 20
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cars = GameObject.FindGameObjectsWithTag ("Car");
		waits = new float[cars.Length];
		int i = 0;
		foreach (GameObject car in cars) {
			waits [i] = car.GetComponent<Movement>().RetrieveWaitTime(); // should be equal to car's waittime
			i += 1;
		}
		mean = Average (waits);
		variance = Variance (waits, mean);
		score = (mFactor * mean + vFactor * variance).ToString ();

	}

	private float Average(float[] nums) {
		float sum = 0;
		if (nums.Length > 1) {
			foreach (float num in nums) {
				sum += num;
			}
			return sum / nums.Length;
		} else
			return 0;
	}

	private float Variance(float[] nums, float average) {
		if (nums.Length > 1) {
			float sumOfSquares = 0;
			foreach (float num in nums) {
				sumOfSquares += (num - average) * (num - average);
			}
			return sumOfSquares / (nums.Length - 1);
		} else
			return 0;
	}
}
