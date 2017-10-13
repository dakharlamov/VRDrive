//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState {

	public enum GripState {open, closed};
	public enum HandID {right, left};

	private HandID handTracked;
	private GripState currentState;

	//Momento both hands
	public HandState(HandID hand){

		handTracked = hand;

		currentState = GripState.open;

	}

	public void updateState(){

		if(handTracked == HandID.left){
			
			float squeezeValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
			if(squeezeValue > 0.5f)
				currentState = GripState.closed;
			else
				currentState = GripState.open;
			
		}else if(handTracked == HandID.right){

			float squeezeValue = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
			if(squeezeValue > 0.5f)
				currentState = GripState.closed;
			else
				currentState = GripState.open;

		}
	
	}

	public GripState getGripState(){
		return currentState;
	}


	public bool checkIsHoldingWheel(ref GameObject hand, ref GameObject steeringWheelOrigin){

		float rMax = 0.275f;
		float rMin = 0.15f;

		Debug.DrawLine(steeringWheelOrigin.transform.position, steeringWheelOrigin.transform.position + (steeringWheelOrigin.transform.forward * rMax), Color.red);
		Debug.DrawLine(steeringWheelOrigin.transform.position, steeringWheelOrigin.transform.position + (steeringWheelOrigin.transform.forward * rMin), Color.blue);

		if(currentState == GripState.closed){

			float dist = Vector3.Magnitude(hand.transform.position - steeringWheelOrigin.transform.position);

			return rMin < dist ? dist < rMax : false;

		}
		return false;
	}


}
