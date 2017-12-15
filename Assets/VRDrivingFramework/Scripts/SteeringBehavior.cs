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


	private Quaternion lastRot;


	// Use this for initialization
	void Start () {

		inputController = this.GetComponent<InputController>();

		initialRotation = steeringWheel.transform.localRotation;
		lastPosLeft = lastPosRight = Vector3.zero;

		wheels = this.GetComponent<WheelController>();


		lastRot = initialRotation;



	}

	void Update () {



		inputController.leftHandState.updateState();
		inputController.rightHandState.updateState();

		if(inputController.leftHandState.checkIsHoldingWheel(ref leftHand, ref steeringWheel)){

			moveSteeringWheel(HandState.HandID.left);

		}else{
			
			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.position);

			localHand.y = 0;

			lastPosLeft = localHand;
		}





		if(inputController.rightHandState.checkIsHoldingWheel(ref rightHand, ref steeringWheel)){
			
			moveSteeringWheel(HandState.HandID.right);


		}else{


			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.position);

			localHand.y = 0;

			lastPosRight = localHand;

		}



		Vector3 leftlocalHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.position);

		leftlocalHand.y = 0;

		lastPosLeft = leftlocalHand;

		Vector3 rightlocalHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.position);

		rightlocalHand.y = 0;


		lastPosRight = rightlocalHand;


		float steering = Mathf.Sin(steeringWheel.transform.localRotation.eulerAngles.y * Mathf.Deg2Rad);


		float angle = steeringWheel.transform.localRotation.eulerAngles.y;

		if(angle > 90){
			angle = 360 - angle;
		}

		if(0 < angle ? angle < 90 : false){
			wheels.updateSteering(steering);
			lastRot = steeringWheel.transform.localRotation;
		}else{
			steeringWheel.transform.localRotation = lastRot;
		}


	}

	void moveSteeringWheel(HandState.HandID hand){

		float angleBetween = 0;
		float direction = 1;


		if(hand == HandState.HandID.right){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(rightHand.transform.position);

			localHand.y = 0;


			Vector3 previousVector = Vector3.Normalize(lastPosRight);

			Vector3 currentVector = Vector3.Normalize(localHand); 

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.up);

			angleBetween = Mathf.Acos(Mathf.Abs(Vector3.Dot(previousVector, currentVector))) * Mathf.Rad2Deg;



		}


		if(hand == HandState.HandID.left){

			Vector3 localHand = steeringWheel.transform.InverseTransformPoint(leftHand.transform.position);

			localHand.y = 0;


			Vector3 previousVector = Vector3.Normalize(lastPosLeft);

			Vector3 currentVector = Vector3.Normalize(localHand);

			Vector3 crossCheck = Vector3.Cross(previousVector, currentVector);

			direction = Vector3.Dot(Vector3.Normalize(crossCheck), Vector3.up);

			angleBetween = Mathf.Acos(Mathf.Abs(Vector3.Dot(previousVector, currentVector))) * Mathf.Rad2Deg;

		}

		float currentRotationY = steeringWheel.transform.localRotation.eulerAngles.y;
		 
		angleBetween *= Mathf.Sign(direction);


		float newRotation = angleBetween + currentRotationY;

		if(newRotation > 360){
			newRotation -= 360;
		}

		if(newRotation < 0){
			newRotation += 360;
		}



		steeringWheel.transform.localRotation = Quaternion.AngleAxis(newRotation, Vector3.up);	// initialRotation









	}

}
