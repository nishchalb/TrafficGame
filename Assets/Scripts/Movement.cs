﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject nextWaypoint;
    public float turnRate = 1.0f;
    public float maxVelocity = 10f;
    public float acceleration = 1.0f;
    public float maxDist;    public float decel;

    private Rigidbody2D rb;
    private Vector3 targetDir;
    private float angDiff;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        targetDir = nextWaypoint.transform.position - transform.position;
        angDiff = Vector2.Angle(transform.up, targetDir);

        //if (Mathf.Abs(angDiff) > .1)
        if (Vector2.Dot(transform.up, targetDir) < .99)
        {
            
            rb.velocity = Vector2.zero;
            Vector3 cross = Vector3.Cross(transform.up, targetDir);
            if (cross.z < 0) angDiff *= -1;
            Debug.Log(angDiff);
            rb.rotation += turnRate * angDiff;
            transform.up = Vector2.Lerp(transform.up, targetDir, .1f);
            // rb.AddForce(-1 * rb.velocity); disabled for now in favor of immediate stopping
        } else
        {
            // Move towards waypoint
            if (rb.velocity.magnitude < maxVelocity)
            {
                rb.AddForce(transform.up*acceleration);
            }
        }

        // Dont run into other cars
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);        float distance = Vector2.Distance(transform.position, hit.point);        if (hit.collider.tag == "Car" && distance < maxDist && Vector2.Dot(rb.velocity, transform.up) > 0)        {
            rb.AddForce(rb.velocity.magnitude * -decel * transform.up * (1 / distance));
            // rb.velocity *= 1 - decel * (1/hit.distance);
            if (Vector2.Dot(rb.velocity, transform.up) < 0 || distance < 1.5)
            {
                rb.velocity = Vector2.zero;
            }        }

        Debug.DrawRay(transform.position, targetDir);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        nextWaypoint  = collision.gameObject.GetComponent<WaypointBehavior>().GetNextWaypoint();
    }
}
