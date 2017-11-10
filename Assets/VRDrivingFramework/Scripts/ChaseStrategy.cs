using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChaseStrategy : MonoBehaviour {

	public abstract Waypoint pickAttack();

}

//TODO: Implement shooting strategy
public class AggressiveStrategy : ChaseStrategy{

	private PlayerTargets targets;

	public AggressiveStrategy(GameObject playerCar){

		targets = playerCar.GetComponent<PlayerTargets>();

	}

	public override Waypoint pickAttack(){

		return (((int)Time.time) % 2 == 0) ? targets.frontWaypont : targets.farFrontWaypont;

	}

}

public class NeutralStrategy : ChaseStrategy{

	private PlayerTargets targets;

	public NeutralStrategy(GameObject playerCar){

		targets = playerCar.GetComponent<PlayerTargets>();

	}

	public override Waypoint pickAttack(){

		int chance = Random.Range(0,4);

		switch(chance){
		case 0:
			return targets.leftWaypont;
			break;
		case 1:
			return targets.farLeftWaypont;
			break;
		case 2:
			return targets.rightWaypont;
			break;
		case 3:
			return targets.farRightWaypont;
			break;
		default:
			return targets.leftWaypont;
			break;
		}


		return targets.frontWaypont;

	}

}


public class DefensiveStrategy : ChaseStrategy{

	private PlayerTargets targets;

	public DefensiveStrategy(GameObject playerCar){

		targets = playerCar.GetComponent<PlayerTargets>();

	}

	public override Waypoint pickAttack(){

		return (((int)Time.time) % 2 == 0) ? targets.backWaypont : targets.farBackWaypont;

	}

}