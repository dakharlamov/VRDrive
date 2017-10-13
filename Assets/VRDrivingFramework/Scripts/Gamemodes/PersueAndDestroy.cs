//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueAndDestroy : MonoBehaviour {

	private Waypoint[] waypoints;

	// Use this for initialization
	void Start () {

		waypoints = GetComponentsInChildren<Waypoint>();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
