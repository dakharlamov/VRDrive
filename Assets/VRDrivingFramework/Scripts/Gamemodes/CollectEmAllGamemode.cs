using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEmAllGamemode : MonoBehaviour {

	public List<GameObject> goodCollectables;
	public List<GameObject> badCollectables;

	//Can go up with health pickup and down
	//by hitting bombs
	private int playerHealth = 100;
	private float playerSpeedMultiplier = 1.0f;
	private int playerPoints = 0;

	// Use this for initialization
	void Start () {

		goodCollectables = new List<GameObject>();
		badCollectables = new List<GameObject>();

		foreach(GameObject collectable in this.GetComponentsInChildren<GameObject>()){

			if(collectable.GetComponent<Collectable>().getCollectableType() == CollectableEffects.TYPE_GOOD){
				goodCollectables.Add(collectable);
			}else{
				badCollectables.Add(collectable);
			}

		}

		
	}
	
	// Update is called once per frame
	void Update () {

		if(playerHealth <= 0){
			//Player dies, game over
		}

		if(goodCollectables.Count == 0){
			//Player got all the points possible, game over
			//player wins
		}

	}

	public void changePlayerHealth(int amount){
		playerHealth += amount;
	}

	public void changePlayerSpeed(bool isIncreasing){

		if(isIncreasing){
			playerSpeedMultiplier *= 1.5f;
		}else{
			playerSpeedMultiplier *= 0.5f;
		}

	}

	public void updatePlayerScore(int amount){
		playerPoints += amount;
	}

	public void removeCollectable(GameObject collectable){

		if(collectable.GetComponent<Collectable>().getCollectableType() == CollectableEffects.TYPE_GOOD){
			goodCollectables.Remove(collectable);
		}else{
			badCollectables.Remove(collectable);
		}

	}

}
