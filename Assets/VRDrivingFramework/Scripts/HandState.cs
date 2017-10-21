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

		// radius of wheel is 0.5f

		float rMax = 0.6f;
		float rMin = 0.3f;



		if(currentState == GripState.closed){

			Vector3 localHand = steeringWheelOrigin.transform.InverseTransformPoint(hand.transform.position);

			Vector3 localSteer = steeringWheelOrigin.transform.localPosition;

			if(Mathf.Abs(localHand.y - localSteer.y) > 2.0f)
				return false;
			
			localHand.y = 0;
			localSteer.y = 0;

			float dist = Vector3.Magnitude(localHand - localSteer);

			//GameObject.Find("DBA").GetComponent<TextMesh>().text = "" + dist;
			//GameObject.Find("DBB").GetComponent<TextMesh>().text = localHand.ToString();
			//GameObject.Find("DBC").GetComponent<TextMesh>().text = localSteer.ToString();

			return rMin < dist ? dist < rMax : false;

		}
		return false;
	}


}
