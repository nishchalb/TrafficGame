using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSignBehavior : MonoBehaviour {

	public Queue<int> carQueue;
	private float time;
	private const int MAX_TIME = 100; 

	// Use this for initialization
	void Start () {
		carQueue = new Queue<int> ();
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate(){
		if (time == 0) {
			if (carQueue. ToArray().Length > 0) {
				int id = carQueue.Dequeue ();
				Debug.Log ("Stop Sign GO: " + id);
				gameObject.SendMessage ("StopSignContinue", id);
				time = MAX_TIME;
			}
		} else {
			time--;
		}
	} 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Car") {
			int id = collision.attachedRigidbody.GetInstanceID ();
			Debug.Log ("Stop Sign: " + id);
			carQueue.Enqueue (id);
			if (time <= 0) {
				time = MAX_TIME;
			}
		} else {
			Debug.Log ("Something is touching this stop sign");
		}
	}
}
