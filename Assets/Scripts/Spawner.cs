using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject waypoint;
    public float spawnRate;
    public GameObject car;
    public int totalCars;

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
            yield return new WaitForSeconds(spawnRate);
            GameObject newCar = Instantiate(car, transform.position, transform.rotation);
            newCar.GetComponent<Movement>().nextWaypoint = waypoint;
        }
    }
}
