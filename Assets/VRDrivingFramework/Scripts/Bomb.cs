using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	private CollectEmAllGamemode gmode;

	void Start () {
		List<int> effects = new List<int>();
		effects.Add(CollectableEffects.DECREASE_SCORE);
		effects.Add(CollectableEffects.DECREASE_HEALTH);
		effects.Add(CollectableEffects.DECREASE_SPEED);
		this.init(effects, 10, CollectableEffects.TYPE_BAD);
		gmode = this.GetComponentInParent<CollectEmAllGamemode>();
	}

	void Update () {
		
	}

	protected override void doAction ()
	{
		//Play explosion particle effect here
	}


	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player"){
			this.onCollected(gmode);
		}
	}

}
