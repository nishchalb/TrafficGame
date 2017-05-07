using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSignBehavior : MonoBehaviour {

	public Queue<int> carQueue;
	private float time;

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
			if (carQueue.Peek () != 0) {
				int id = carQueue.Dequeue ();
				Debug.Log ("Stop Sign GO: " + id);
				gameObject.SendMessage ("StopSignContinue", id);
				time = 200;
			}
		} else {
			time--;
		}
	} 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Movement") {
			int id = collision.attachedRigidbody.GetInstanceID ();
			Debug.Log ("Stop Sign: " + id);
			carQueue.Enqueue (id);
			if (time <= 0) {
				time = 200;
			}
		} else {
			Debug.Log ("Something is touching this stop sign");
		}
	}
}
