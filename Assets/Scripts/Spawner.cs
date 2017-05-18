using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject waypoint;
    public float spawnRate;
    public GameObject car;
    public int totalCars;

    public Score score;

    private Collider2D coll;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        for (int i = 0; i<totalCars; i++)
        {
            
            coll = Physics2D.OverlapCircle(transform.position, 2, LayerMask.GetMask("Car"));
            if (coll == null)
            {
                GameObject newCar = Instantiate(car, transform.position, transform.rotation);
                newCar.GetComponent<Movement>().nextWaypoint = waypoint;
                newCar.GetComponent<Movement>().score = score;
            } else
            {
                i -= 1;
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
