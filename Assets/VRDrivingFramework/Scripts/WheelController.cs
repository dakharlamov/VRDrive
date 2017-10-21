//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

	public UnityStandardAssets.Vehicles.Car.CarController carRef;

	private float steeringIntesity;

	private float acceleration;

	private float braking;

	// Use this for initialization
	void Start () {

		steeringIntesity = 0;
	}

	//TODO: Implement Handbrake via lever in game
	void FixedUpdate () {

		carRef.Move(steeringIntesity, acceleration, braking, 0);
	}

	public void updateSteering(float intensity){

		steeringIntesity = intensity;

	}

	public void updateAcceleration(float intensity){

		acceleration = intensity;

	}

	public void updateBraking(float intensity){

		braking = intensity;

	}

}
