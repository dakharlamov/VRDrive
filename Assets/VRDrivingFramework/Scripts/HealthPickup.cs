using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Collectable {
	
	private CollectEmAllGamemode gmode;

	void Start () {
		List<int> effects = new List<int>();
		effects.Add(CollectableEffects.INCREASE_HEALTH);
		this.init(effects, 10, CollectableEffects.TYPE_GOOD);
		gmode = this.GetComponentInParent<CollectEmAllGamemode>();
	}

	void Update () {

	}

	protected override void doAction ()
	{
		//Play health up sound and play particles
	}


	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player"){
			this.onCollected(gmode);
		}
	}

}
