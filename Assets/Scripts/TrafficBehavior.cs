using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficBehavior : MonoBehaviour {

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Debug.Log("here");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        Debug.Log(hit.distance);
        if (hit.collider.tag == "Car" && hit.distance < 5 && Vector2.Dot(rb.velocity, transform.up) > 0)
        {
            rb.AddForce(-1 * transform.up * (5-hit.distance));
            if (Vector2.Dot(rb.velocity, transform.up) < 0) rb.velocity = Vector2.zero;
        }
    }
}
