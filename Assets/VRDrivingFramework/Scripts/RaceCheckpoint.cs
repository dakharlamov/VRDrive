using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour {

	public CheckpointRace gameMode;

	void OnTriggerEnter(Collider col){

		//Talk to gamemode and inform about checkpoint entry, if this checkpoint's id does not match with next, deduct time

		if(col.tag == "Player"){

			gameMode.checkpointReached(this.gameObject.name);


		}

	}

}
