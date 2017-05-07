using System.Collections;
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
	private bool stopped;
    private bool isTurning;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		stopped = false;
        isTurning = false;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
		if (stopped) {
			return;
		}
        Vector2 offset = nextWaypoint.GetComponent<WaypointBehavior>().GetWaypointOffset();
        targetDir = nextWaypoint.transform.position + new Vector3(offset.x, offset.y, 0) - transform.position;
        Debug.DrawRay(transform.position, targetDir);
        angDiff = Vector2.Angle(transform.up, targetDir.normalized);

        float cosine = Vector2.Dot(transform.up, targetDir.normalized);

        // Check if we need to turn
        //if (Mathf.Abs(angDiff) > .1)
        if (cosine < .995 && !isTurning)
        {
            
            rb.velocity = Vector2.zero;
            StartCoroutine(Turn(transform.up, targetDir.normalized, transform));
            // Debug.Log(cosine);
            // transform.up = new Vector3(targetDir.normalized.x, targetDir.normalized.y, 0);
            // rb.rotation += turnRate * angDiff;
            ///transform.up = Vector2.Lerp(transform.up, targetDir, .1f);
            // rb.AddForce(-1 * rb.velocity); disabled for now in favor of immediate stopping
        } else
        {
            // Move towards waypoint
            if (rb.velocity.magnitude < maxVelocity && !isTurning)
            {
                rb.AddForce(transform.up*acceleration);
            }
        }
			// Check if we need to turn
			//if (Mathf.Abs(angDiff) > .1)
			if (cosine < .99) {
	            
				rb.velocity = Vector2.zero;
				Vector3 cross = Vector3.Cross (transform.up, targetDir);
				if (cross.z < 0)
					angDiff *= -1;
				Debug.Log (angDiff);
				rb.rotation += turnRate * angDiff;
				transform.up = Vector2.Lerp (transform.up, targetDir, .1f);
				// rb.AddForce(-1 * rb.velocity); disabled for now in favor of immediate stopping
			} else {
				// Move towards waypoint
				if (rb.velocity.magnitude < maxVelocity) {
					rb.AddForce (transform.up * acceleration);
				}
			}

			// Dont run into other cars
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up);		float distance = Vector2.Distance (transform.position, hit.point);
		if (hit.collider) {			if (hit.collider.tag == "Car" && distance < maxDist && Vector2.Dot (rb.velocity, transform.up) > 0) {
				rb.AddForce (rb.velocity.magnitude * -decel * transform.up * (1 / distance));
				// rb.velocity *= 1 - decel * (1/hit.distance);
				if (Vector2.Dot (rb.velocity, transform.up) < 0 || distance < 1.5) {
					rb.velocity = Vector2.zero;
				}			}
		}
		Debug.DrawRay (transform.position, targetDir);
	}


    IEnumerator Turn(Vector3 start, Vector3 end, Transform tform)
    {
        isTurning = true;
        for (float f = 0f; f <= 1; f += 0.2f)
        {
            transform.up = Vector2.Lerp(start, end, f);
            yield return null;
        }
        //Debug.Log(Vector2.Dot(transform.up, end));
        isTurning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Waypoint")
        {
            Debug.Log("collision");
            nextWaypoint = collision.gameObject.GetComponent<WaypointBehavior>().GetNextWaypoint();
        }
		else {
			//Stop the car's velocity
			Debug.Log("STOP");
			Vector2 v;
			v.y = 0;
			v.x = 0;
			rb.velocity = v;
			stopped = true;
		}
    }

	public void StopSignContinue(){
		//If the stop sign calls us, continue moving.
		Debug.Log ("GOOD TO GO");
		stopped = false;
	}
}
