using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSignBehavior : MonoBehaviour {

	public Queue<GameObject> carQueue;
	private float time;
	private const int MAX_TIME = 100; 

	// Use this for initialization
	void Start () {
		carQueue = new Queue<GameObject> ();
		time = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate(){
		if (time == 0) {
			if (carQueue. ToArray().Length > 0) {
				GameObject car = carQueue.Dequeue ();
				Debug.Log ("Stop Sign GO: " + car.GetInstanceID());
				car.GetComponent<Movement> ().StopSignContinue ();
				time = MAX_TIME;
			}
		} else {
			time--;
		}
	} 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Car") {
			GameObject id = collision.attachedRigidbody.gameObject;
			Debug.Log ("Stop Sign: " + id.GetInstanceID());
			carQueue.Enqueue (id);
			if (time <= 0) {
				time = MAX_TIME;
			}
		} else {
			Debug.Log ("Something is touching this stop sign");
		}
	}
}
