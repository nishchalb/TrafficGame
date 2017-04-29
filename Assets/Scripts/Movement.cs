using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject nextWaypoint;
    public float turnRate = 1.0f;
    public float maxVelocity = 10f;
    public float acceleration = 1.0f;

    private Rigidbody2D rb;
    private Vector3 targetDir;
    private float angDiff;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        targetDir = nextWaypoint.transform.position - transform.position;
        angDiff = Vector2.Angle(transform.up, targetDir);
    }

    private void FixedUpdate()
    {
        
        
        //if (Mathf.Abs(angDiff) > .1)
        if (Vector2.Dot(transform.up, targetDir) < .99)
        {
            Debug.Log(Vector2.Dot(transform.up, targetDir));
            rb.velocity = Vector2.zero;
            Vector3 cross = Vector3.Cross(transform.up, targetDir);
            if (cross.z < 0) angDiff *= -1;
            rb.rotation += turnRate * angDiff;
            transform.up = Vector2.Lerp(transform.up, targetDir, .1f);
            // rb.AddForce(-1 * rb.velocity);
        } else
        {
            // Move towards waypoint
            if (rb.velocity.magnitude < maxVelocity)
            {
                rb.AddForce(transform.up*acceleration);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        nextWaypoint  = collision.gameObject.GetComponent<WaypointBehavior>().GetNextWaypoint();
    }
}
