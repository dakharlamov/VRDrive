//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBarBehavior : MonoBehaviour {

	public GameObject redlight01;
	public GameObject redlight02;
	public GameObject bluelight01;
	public GameObject bluelight02;


	private bool cycle = true;

	// Use this for initialization
	void Start () {

		InvokeRepeating("cycleToNext", 0.0f, 0.5f);

	}

	// Update is called once per frame
	void Update () {
		if(cycle){

			redlight01.SetActive(true);
			redlight02.SetActive(false);
			bluelight01.SetActive(true);
			bluelight02.SetActive(false);

		}else{

			redlight01.SetActive(false);
			redlight02.SetActive(true);
			bluelight01.SetActive(false);
			bluelight02.SetActive(true);
			
		}

	}

	private void cycleToNext(){

		cycle = !cycle;

	}

}
