//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {


	//Container Class for waypoints, does nothing

	public Vector3 getPosition(){
		return this.transform.position;
	}

	public Transform getTransform(){
		return this.transform;
	}

}
