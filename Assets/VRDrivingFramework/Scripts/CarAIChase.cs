using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIChase : MonoBehaviour {

	private ChaseStrategy chaseStrategy;

	private UnityStandardAssets.Vehicles.Car.CarAIControl controls;

	private int hitPoints;

	private bool isDead;

	private bool isDecoupled;


	public void init(ChaseStrategy chaseStrat){

		controls = gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarAIControl>();

		chaseStrategy = chaseStrat;

		controls.SetTarget(chaseStrat.pickAttack().getTransform());

		isDead = isDecoupled = false;

		hitPoints = 5;

	}

	public void UpdateAI(){

		if(hitPoints <= 0){
			controls.stopDriving();
			isDead = true;
		}

		if(controls.hasReachedCheckpoint()){
			chooseNextTarget();
		}


	}


	void Update () {
		if(isDead && isDecoupled){
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

	public void damageCar(){
		hitPoints--;
	}

	private void destroyThisAI(){
		Destroy(this.gameObject, 3.0f);
	}

	private void chooseNextTarget(){

		controls.SetTarget(chaseStrategy.pickAttack().getTransform());

	}




}
