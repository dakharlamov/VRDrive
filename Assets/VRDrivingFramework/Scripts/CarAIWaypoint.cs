//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarAIWaypoint : MonoBehaviour {

	private WaypointStrategy travelStrategy;

	private UnityStandardAssets.Vehicles.Car.CarAIControl controls;


	private int hitPoints;

	private bool isDead;
	private bool isDecoupled;

	public void init (WaypointStrategy travelStrat) {

		controls = gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarAIControl>();

		travelStrategy = travelStrat;

		controls.SetTarget(travelStrategy.pickNextWaypoint().getTransform());

		isDead = isDecoupled = false;

		hitPoints = 3;

	}

	public void UpdateAI(){

		if(hitPoints <= 0){
			controls.stopDriving();
			isDead = true;
		}

		if(controls.hasReachedCheckpoint()){
			changeWaypoint();
		}

	}

	//This is only for decoupled use!
	void Update(){
		
		if(isDead && isDecoupled){
			//TODO: explode and destroy
	
			destroyThisAI();
		}

		playExp();
	}

	private bool animStarted = false;
	private void playExp(){
		if(isDead && !animStarted){

			animStarted = true;

			foreach(ParticleSystem parts in gameObject.GetComponentsInChildren<ParticleSystem>()){

				parts.Play();

			}

		}
	}


	public bool currentlyDead(){
		return isDead;
	}

	public void verifyDecoupling(){
		isDecoupled = true;
	}

	public void changeWaypoint(){
		controls.SetTarget(travelStrategy.pickNextWaypoint().getTransform());
	}

	public void damageCar(){
		hitPoints--;
	}

	private void destroyThisAI(){
		Destroy(this.gameObject, 3.0f);
	}

}
