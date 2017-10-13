//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

	public UnityStandardAssets.Vehicles.Car.CarController carRef;

	private float steeringIntesity;

	// Use this for initialization
	void Start () {

		steeringIntesity = 0;
	}

	//TODO: Implement Handbrake via lever in game
	void FixedUpdate () {

		float acceleration = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		
		float brake = -1 * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		Debug.Log(brake);

		carRef.Move(steeringIntesity, acceleration, brake, 0);
	}

	public void updateSteering(float intensity){

		steeringIntesity = intensity;

	}

}
