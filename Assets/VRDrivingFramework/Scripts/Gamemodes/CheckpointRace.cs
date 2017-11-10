using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRace : MonoBehaviour {

	private List<RaceCheckpoint> checkpoints;

	// Use this for initialization
	void Start () {

		checkpoints = new List<RaceCheckpoint>(GetComponentsInChildren<RaceCheckpoint>());

		foreach(RaceCheckpoint checkpoint in checkpoints){
			checkpoint.gameMode = this;
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if(checkpoints.Count <= 0){

			//Handle end of race
		}

		
	}

	public void checkpointReached(string goName){

		if(checkpoints[0].gameObject.name == goName){
			//Player hit the correct checkpoint
			// Add score accordingly

			checkpoints.RemoveAt(0);
		}else{

			foreach(RaceCheckpoint check in checkpoints){
				if(check.gameObject.name == goName){
					//Player hit incorrect checkpoint
					// Penalize score accordingly
					checkpoints.Remove(check);
					break;
				}
			}

		}
	}
}
