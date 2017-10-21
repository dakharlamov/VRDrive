//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SteeringBehavior : MonoBehaviour {

	public GameObject steeringWheel;

	public InputController inputController;

	public GameObject leftHand;
	public GameObject rightHand;


	private Quaternion initialRotation;

	private Vector3 lastPosLeft;
	private Vector3 lastPosRight;

	private WheelController wheels;

	// Use this for initialization
	void Start () {

		inputController = this.GetComponent<InputController>();

		initialRotation = steeringWheel.transform.localRotation;
		lastPosLeft = lastPosRight = Vector3.zero;

		wheels = this.GetComponent<WheelController>();

	}

	// Update is called once per frame
	void Update () {


		inputController.leftHandState.updateState();
		inputController.rightHandState.updateState();

		if(inputController.leftHandState.getGripState() == HandState.GripState.closed){

			if(inputController.leftHandState.checkIsHoldingWheel(ref leftHand, ref steeringWheel)){

				moveSteeringWheel(HandState.HandID.left);

			}
		}else{

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.localPosition);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			lastPosLeft = deYed;
		}

		if(inputController.rightHandState.getGripState() == HandState.GripState.closed){

			if(inputController.rightHandState.checkIsHoldingWheel(ref rightHand, ref steeringWheel)){
				
				moveSteeringWheel(HandState.HandID.right);

			}
		}else{



			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.localPosition);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			lastPosRight = deYed;
		}

	}

	void moveSteeringWheel(HandState.HandID hand){

		float angleBetween = 0;
		Vector3 steeringWheelOrigin = steeringWheel.transform.localPosition;

		if(hand == HandState.HandID.right){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.localPosition);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			steeringWheelOrigin.y = 0;

			Vector3 previousVector = Vector3.Normalize(lastPosRight - steeringWheelOrigin);

			Vector3 currentVector = Vector3.Normalize(deYed - steeringWheelOrigin);

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			float direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.Normalize(steeringWheel.transform.up));

			angleBetween = Mathf.Sign(direction) * Mathf.Acos(Vector3.Dot(previousVector, currentVector)) * Mathf.Rad2Deg * Mathf.PI;

			lastPosRight = deYed;
		}


		if(hand == HandState.HandID.left){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.localPosition);

			localHand.y = 0;

			Vector3 deYed = steeringWheel.transform.TransformPoint(localHand);

			steeringWheelOrigin.y = 0;

			Vector3 previousVector = Vector3.Normalize(lastPosLeft - steeringWheelOrigin);

			Vector3 currentVector = Vector3.Normalize(deYed - steeringWheelOrigin);

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			float direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.Normalize(steeringWheel.transform.up));

			angleBetween = Mathf.Sign(direction) * Mathf.Acos(Vector3.Dot(previousVector, currentVector)) * Mathf.Rad2Deg * Mathf.PI;

			lastPosLeft = deYed;
		}

		float currentRotationY = steeringWheel.transform.localRotation.eulerAngles.y;
		 
		float newRotation = angleBetween + currentRotationY;



		//260
		//90
		wheels.updateSteering(newRotation * Mathf.Deg2Rad);
		steeringWheel.transform.localRotation = initialRotation * Quaternion.AngleAxis(newRotation, Vector3.up);






	}

}
