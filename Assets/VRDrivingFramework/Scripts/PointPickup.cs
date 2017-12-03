using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickup : Collectable {

	private CollectEmAllGamemode gmode;

	void Start () {
		List<int> effects = new List<int>();
		effects.Add(CollectableEffects.INCREASE_SCORE);
		this.init(effects, 100, CollectableEffects.TYPE_GOOD);
		gmode = this.GetComponentInParent<CollectEmAllGamemode>();
	}

	void Update () {

	}

	protected override void doAction ()
	{
		//Play point increase sound
	}


	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player"){
			this.onCollected(gmode);
		}
	}
}
