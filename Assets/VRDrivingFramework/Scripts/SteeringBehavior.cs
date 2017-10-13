//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: Make wheel rotation dependent on steering wheel rotation

public class SteeringBehavior : MonoBehaviour {

	public GameObject steeringWheel;

	public GameObject leftHand;
	public GameObject rightHand;

	private HandState leftHandState;
	private HandState rightHandState;

	private Quaternion initialRotation;

	private Vector3 lastPosLeft;
	private Vector3 lastPosRight;

	// Use this for initialization
	void Start () {
		leftHandState = new HandState(HandState.HandID.left);
		rightHandState = new HandState(HandState.HandID.right);

		initialRotation = steeringWheel.transform.localRotation;
		lastPosLeft = lastPosRight = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		leftHandState.updateState();
		rightHandState.updateState();

		if(leftHandState.getGripState() == HandState.GripState.closed){

			if(leftHandState.checkIsHoldingWheel(ref leftHand, ref steeringWheel)){

				moveSteeringWheel(HandState.HandID.left);

			}


		}else{
			lastPosLeft = leftHand.transform.position;
		}

		if(rightHandState.getGripState() == HandState.GripState.closed){

			if(rightHandState.checkIsHoldingWheel(ref rightHand, ref steeringWheel)){

				moveSteeringWheel(HandState.HandID.right);

			}
		}else{
			lastPosRight = rightHand.transform.position;
		}

	}

	//TODO: ROTATE WHEEL ANCHORS TOO
	void moveSteeringWheel(HandState.HandID hand){

		float angleBetween = 0;
		Vector3 steeringWheelOrigin = steeringWheel.transform.position;

		if(hand == HandState.HandID.right){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.position);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			steeringWheelOrigin.y = 0;

			Vector3 previousVector = Vector3.Normalize(lastPosRight - steeringWheelOrigin);

			Vector3 currentVector = Vector3.Normalize(deYed - steeringWheelOrigin);

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			float direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.Normalize(steeringWheel.transform.up));

			angleBetween = Mathf.Sign(direction) * Mathf.Acos(Vector3.Dot(previousVector, currentVector)) * Mathf.Rad2Deg * 2.17f;

			lastPosRight = deYed;
		}


		if(hand == HandState.HandID.left){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.position);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			steeringWheelOrigin.y = 0;

			Vector3 previousVector = Vector3.Normalize(lastPosLeft - steeringWheelOrigin);

			Vector3 currentVector = Vector3.Normalize(deYed - steeringWheelOrigin);

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			float direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.Normalize(steeringWheel.transform.up));

			angleBetween = Mathf.Sign(direction) * Mathf.Acos(Vector3.Dot(previousVector, currentVector)) * Mathf.Rad2Deg * 2.17f;

			lastPosLeft = deYed;
		}

		float currentRotationY = steeringWheel.transform.localRotation.eulerAngles.y;
		 
		float newRotation = angleBetween + currentRotationY;

		//260
		//90

		Debug.Log(newRotation);
		//Vector3 eu = new Vector3(initialRotation.eulerAngles.x, newRotation, initialRotation.eulerAngles.z);
		steeringWheel.transform.localRotation = initialRotation * Quaternion.AngleAxis(newRotation, Vector3.up);//Quaternion.Euler(eu);


	}

}
