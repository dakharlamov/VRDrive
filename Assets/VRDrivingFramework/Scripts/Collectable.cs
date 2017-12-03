using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	protected int effectAmount;

	protected List<int> collectableEffects;

	protected int collectableType;

	protected void init(List<int> collectableEffectsList, int amount, int type){
		effectAmount = amount;
		collectableEffects = collectableEffectsList;
		collectableType = type;
	}

	public void onCollected(CollectEmAllGamemode gmode){
		foreach(int ce in collectableEffects){
			switch(ce){
			case CollectableEffects.NONE:
				break;
			case CollectableEffects.INCREASE_HEALTH:
				gmode.changePlayerHealth(effectAmount);
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.DECREASE_HEALTH:
				gmode.changePlayerHealth(effectAmount);
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.INCREASE_SPEED:
				gmode.changePlayerSpeed(true);
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.DECREASE_SPEED:
				gmode.changePlayerSpeed(false);
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.INCREASE_ENEMY_SPEED:
				//No default behavior yet implemented
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.DECREASE_ENEMY_SPEED:
				//No default behavior yet implemented
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.INCREASE_WEAPON_DAMAGE:
				//No default behavior yet implemented
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.DECREASE_WEAPON_DAMAGE:
				//No default behavior yet implemented
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.INCREASE_SCORE:
				gmode.updatePlayerScore(effectAmount);
				gmode.removeCollectable(this.gameObject);

				break;
			case CollectableEffects.DECREASE_SCORE:
				gmode.removeCollectable(this.gameObject);
				gmode.updatePlayerScore(effectAmount);

				break;
			default:
				gmode.removeCollectable(this.gameObject);

				break;
			}
			doAction();
		}
	}
	public int getCollectableType(){
		return collectableType;
	}

	protected virtual void doAction(){}

}
