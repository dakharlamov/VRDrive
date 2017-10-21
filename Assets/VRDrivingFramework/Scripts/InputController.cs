using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public HandState leftHandState;
	public HandState rightHandState;
	public InputController bothHands;
	public WheelController wheelController;

	private bool A_buttonToggle;
	private bool A_buttonLocked;

	private bool accelerationLock;

	// Use this for initialization
	void Start () {
		leftHandState = new HandState(HandState.HandID.left);
		rightHandState = new HandState(HandState.HandID.right);

		wheelController = this.GetComponent<WheelController>();

		bothHands = this;

		accelerationLock = false;

		A_buttonToggle = false;
		A_buttonLocked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		leftHandState.updateState();
		rightHandState.updateState();

		if(OVRInput.Get(OVRInput.Button.One) && !A_buttonLocked){
			A_buttonToggle = !A_buttonToggle;
			A_buttonLocked = true;
			Invoke("unlockButtonA", 0.5f);
		}

		float acceleration = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if(!accelerationLock)
			wheelController.updateAcceleration(acceleration);


		float brake = -1 * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

		wheelController.updateBraking(brake);


	}

	public float getRightTriggerSqueeze(){
		return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
	}

	public void lockAcceleration(){

		accelerationLock = true;

	}

	public void unlockAcceleration(){

		accelerationLock = false;

	}


	private void unlockButtonA(){
		A_buttonLocked = false;
	}


	public bool getAButtonToggleState(){

		return A_buttonToggle;

	}

}
