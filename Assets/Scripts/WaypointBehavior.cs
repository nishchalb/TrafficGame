﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour {

    [System.Serializable]
    public struct NextWaypoint
    {
        public GameObject wp;
        public float rangeMin;
        public float rangeMax;
    }

    public NextWaypoint[] nextWaypoints;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetNextWaypoint()
    {
        float val = Random.value;
        foreach (NextWaypoint waypoint in nextWaypoints)
        {
            if (waypoint.rangeMin <= val && waypoint.rangeMax >= val) return waypoint.wp;
        }
        if (nextWaypoints.Length == 0) return null; //I feel so dirty doing this
        return nextWaypoints[0].wp;
    }

    public bool isTerminal()
    {
        return nextWaypoints.Length == 0;
    }

    public Vector2 GetWaypointOffset()
    {
        return GetComponent<BoxCollider2D>().offset;
    }
}
