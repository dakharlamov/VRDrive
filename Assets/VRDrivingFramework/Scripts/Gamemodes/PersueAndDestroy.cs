//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueAndDestroy : MonoBehaviour {

	private Waypoint[] waypoints;
	private CarAIWaypoint[] aiCars;
	private List<CarAIWaypoint> carList;

	// Use this for initialization
	void Start () {
		
		aiCars = GetComponentsInChildren<CarAIWaypoint>();

		waypoints = this.GetComponentsInChildren<Waypoint>();

		if(waypoints.GetLength(0) < 1){
			Debug.LogError("No waypoints found as children of gamemode container.");
			return;
		}

		foreach(CarAIWaypoint car in aiCars){
			car.init(new SequentialWaypointStrategy(ref waypoints, true));
		}

		carList = new List<CarAIWaypoint>(aiCars);

	}
	
	// Update is called once per frame
	void Update () {

		foreach(CarAIWaypoint car in carList){
			car.UpdateAI();

			if(car.currentlyDead()){
				carList.Remove(car);
				car.verifyDecoupling();
			}

		}

	}


}
