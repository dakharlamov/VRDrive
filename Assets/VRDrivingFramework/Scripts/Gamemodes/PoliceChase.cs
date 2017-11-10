using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceChase : MonoBehaviour {

	private GameObject playerCar;

	private CarAIChase[] copCars;

	private List<CarAIChase> copList;

	// Use this for initialization
	void Start () {

		copCars = GetComponentsInChildren<CarAIChase>();

		foreach(CarAIChase car in copCars){

			int chance = Random.Range(0,3);

			switch(chance){
			case 0:
				car.init(new AggressiveStrategy(car.gameObject));
				break;
			case 1:
				car.init(new NeutralStrategy(car.gameObject));
				break;
			case 2:
				car.init(new DefensiveStrategy(car.gameObject));
				break;
			default:
				car.init(new AggressiveStrategy(car.gameObject));
				break;

			}

			
		}

		copList = new List<CarAIChase>(copCars);

	}
	
	// Update is called once per frame
	void Update () {
		if(copList.Count > 0){
			foreach(CarAIChase car in copList){
				car.UpdateAI();

				if(car.currentlyDead()){
					copList.Remove(car);
					car.verifyDecoupling();
				}

			}
		}
		
	}
}
