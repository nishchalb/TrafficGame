using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Transform nextWaypoint;
    public float turnRate = 1.0f;
    public float maxVelocity = 10f;
    public float acceleration = 1.0f;

    private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        Vector3 targetDir = nextWaypoint.position - transform.position;
        float angDiff = Vector2.Angle(transform.up, targetDir);
        if (angDiff != 0)
        {
            Vector3 cross = Vector3.Cross(transform.up, targetDir);
            if (cross.z < 0) angDiff *= -1;
            rb.rotation += turnRate * angDiff;
        } else
        {
            // Move towards waypoint
            if (rb.velocity.magnitude < maxVelocity)
            {
                rb.AddForce(transform.up*acceleration);
            }
        }
        
    }
}
