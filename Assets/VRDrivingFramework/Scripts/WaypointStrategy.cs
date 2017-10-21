using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaypointStrategy : MonoBehaviour {

	public abstract Waypoint pickNextWaypoint();

}

//TODO: Only one waypoint handler
//		Empty queue handler for non loopers
public class SequentialWaypointStrategy : WaypointStrategy{

	private Queue<Waypoint> waypoints;
	private bool looping;

	public SequentialWaypointStrategy(ref Waypoint[] ways, bool isLooping){

		waypoints = new Queue<Waypoint>(ways);
		looping = isLooping;
	}

	public override Waypoint pickNextWaypoint ()
	{

		Waypoint holder = waypoints.Dequeue();

		if(looping){

			waypoints.Enqueue(holder);

		}

		return holder;

	}
		
}


public class RandomWaypointStrategy : WaypointStrategy{

	private List<Waypoint> waypoints;
	private bool halting;
	private int length;
	private Waypoint currentWaypoint;

	public RandomWaypointStrategy(ref Waypoint[] ways, bool doesHalt){

		waypoints = new List<Waypoint>(ways);
		halting = doesHalt;

	}

	public override Waypoint pickNextWaypoint()
	{
		int idx = Random.Range(0, length - 1);
		if(!currentWaypoint){
			currentWaypoint = waypoints[idx];
			if(halting){
				waypoints.RemoveAt(idx);
				length--;
			}
		}


		if(!halting){

			if(currentWaypoint == waypoints[idx]){
				idx++;
				currentWaypoint = waypoints[idx];
			}
		}else{

			currentWaypoint = waypoints[idx];
			waypoints.RemoveAt(idx);
			length--;
		}

		return currentWaypoint;
	}


}
//TODO: Implement looping
public class ShortestStaticPathWaypointStrategy : WaypointStrategy{

	private List<Waypoint> waypoints;
	private bool looping;
	private int length;
	private GameObject ai;

	public ShortestStaticPathWaypointStrategy(ref Waypoint[] ways, GameObject AI, bool isLooping){

		ai = AI;

		waypoints = new List<Waypoint>(ways);
		looping = isLooping;


	}

	public override Waypoint pickNextWaypoint ()
	{
		float sdist = float.MaxValue;
		Waypoint cn = new Waypoint();
		foreach (Waypoint way in waypoints) {

			float dist = Utilities.LB2V(ai.transform.position, way.getPosition());
			if(sdist > dist){
				sdist = dist;
				cn = way;
			}
		}

		waypoints.Remove(cn);

		return cn;
		
	}

}
