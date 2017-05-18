using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public string score;

	public float variance;
	public float mean;
	public int vFactor;
	public int mFactor;
	public Text viewscore;
    public Text viewcars;
    private int numSamples = 0;
    private float mean2;
    private int totalCars;

    public Spawner[] spawners;
    public int carsLeft;

    public Dictionary<int, float> waitTimes;

	private float[] waits;
	private GameObject[] cars; // make into list for variance

	// Use this for initialization
	void Start () {
		mean = 0;
		variance = 0;
		vFactor = 10;
		mFactor = 20;
        carsLeft = 0;
        foreach(Spawner s in spawners)
        {
            carsLeft += s.totalCars;
        }
        totalCars = carsLeft;
        waitTimes = new Dictionary<int, float>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        numSamples = 0;
        mean = 0;
        mean2 = 0;
		foreach (int key in waitTimes.Keys) {
			float x = waitTimes[key]; // should be equal to car's waittime
            numSamples += 1;
            float delta = x - mean;
            mean += delta / numSamples;
            float delta2 = x - mean;
            mean2 += delta * delta2;
		}

        variance = numSamples >= 2 ? mean2 / (numSamples - 1) : 0;

        //float scoreNum = mFactor * mean + vFactor * variance;
        //scoreNum = carsLeft == totalCars ? scoreNum / cars.Length;


        score = ((int)((mFactor * mean + vFactor * variance)/100f)).ToString ();
		viewscore.text = score;
        viewcars.text = carsLeft.ToString();

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
