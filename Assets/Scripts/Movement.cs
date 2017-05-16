using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject nextWaypoint;
    public float turnRate = 1.0f;
    public float maxVelocity = 10f;
    public float acceleration = 1.0f;
    public float maxDist;
    public float decel;
	public float waitTime;


    private Rigidbody2D rb;
    private Vector3 targetDir;
    private float angDiff;
    private bool stopped;
    private bool isTurning;
	private float isWaiting;



    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stopped = false;
        isTurning = false;
		waitTime = 0;
		isWaiting = -1;
    }

    // Update is called once per frame
    void Update()
    {
		if (rb.velocity.magnitude == 0) {
			Debug.Log ("car is stopped");
			if (isWaiting == -1) {
				isWaiting = Time.time;
			}
		} else if (isWaiting != -1) {
			Debug.Log ("car moving again");
			waitTime = waitTime + Time.time - isWaiting;
			isWaiting = -1;
		}

    }

    private void FixedUpdate()
    {
        rb.WakeUp();

		// Dont run into other cars
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
		Debug.DrawRay(transform.position, transform.up, Color.green);
		float distance = Vector2.Distance(transform.position, hit.point);
		if (hit.collider != null && hit.collider.tag == "LargeBody" && distance < maxDist  && Vector2.Dot(rb.velocity, transform.up) > 0)
		{
			Debug.Log ("HIT");
			return;
		}

		//If it's waiting, don't move
		if (stopped) {
			return;
		}

        HandleWaypointCollision();
        Vector2 offset = nextWaypoint.GetComponent<WaypointBehavior>().GetWaypointOffset();
        targetDir = nextWaypoint.transform.position + new Vector3(offset.x, offset.y, 0) - transform.position;
        Debug.DrawRay(transform.position, targetDir);

        float cosine = Vector2.Dot(transform.up, targetDir.normalized);

        // Check if we need to turn
        if (cosine < .995 && !isTurning)
        {

            rb.velocity = Vector2.zero;
            StartCoroutine(Turn(transform.up, targetDir.normalized, transform));
        }
        else
        {
            // Move towards waypoint
            if (rb.velocity.magnitude < maxVelocity && !isTurning)
            {
                rb.AddForce(transform.up * acceleration);
            }
        }
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
		//if (collision.tag == "Waypoint") {
		//	Debug.Log ("collision");
		//	nextWaypoint = collision.gameObject.GetComponent<WaypointBehavior> ().GetNextWaypoint ();
		//}
        if (collision.tag == "StopSign")
		{
			//Stop the car's velocity
			Debug.Log("Stop Sign");
			stopped = true;
		}
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "LightZoneVertical")
        {
            //Check the state of the light
            GameObject light = collision.gameObject.transform.parent.gameObject;
            //Debug.Log(light.GetComponent<TrafficLight>().vertical);
            
            if (light.GetComponent<TrafficLight>().vertical == "green")
            {
                stopped = false;
            }
            else
            {
                stopped = true;
            }
        }
        if (collision.tag == "LightZoneHorizontal")
        {
            //Check the state of the light
            GameObject light = collision.gameObject.transform.parent.gameObject;
            Debug.Log(light.GetComponent<TrafficLight>().horizontal);
            if (light.GetComponent<TrafficLight>().horizontal == "green")
            {
                stopped = false;
            }
            else
            {
                
                stopped = true;
            }
        }
    }

    private void HandleWaypointCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .01f);
        foreach (Collider2D coll in colliders)
        {
            if (coll.tag == "Waypoint" && coll.gameObject.GetInstanceID() == nextWaypoint.GetInstanceID())
            {
                Debug.Log("collision");
                nextWaypoint = coll.gameObject.GetComponent<WaypointBehavior>().GetNextWaypoint();
                if (nextWaypoint == null)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void StopSignContinue(){
		//If the stop sign calls us, continue moving.
		Debug.Log ("GOOD TO GO");
		stopped = false;
	}

	public float RetrieveWaitTime() {
		return waitTime;
	}
}
