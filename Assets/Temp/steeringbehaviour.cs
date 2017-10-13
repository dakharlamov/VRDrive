//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringbehaviour : MonoBehaviour {

	private float rot;

	// Use this for initialization
	void Start () {

		rot = 0.0f;

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.A) && rot > -25.0f){
			rot -= 100.0f * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.D) && rot < 25.0f){

			rot += 100.0f * Time.deltaTime;
		}

		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){

			rot = Utilities.clampedLerp(rot, 0, Time.deltaTime * 25.0f);

		}

		
		this.transform.localRotation = Quaternion.AngleAxis(rot, Vector3.up);
		
	}
}
