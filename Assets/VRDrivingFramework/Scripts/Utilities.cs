//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities{

	// Linear travel
	public static Vector3 travelTo(Vector3 frm, Vector3 to, float speed){
		Vector3 dir = Vector3.Normalize(to - frm);
		return frm + (dir * speed);
	}

	// Psuedo Linear Travel, if from = from' + to, then it is exp travel
	public static Vector3 clampedLerp(Vector3 frm, Vector3 to, float speed){
		Vector3 prop = Vector3.Lerp(frm, to, speed);
		float dist = Vector3.Magnitude(prop - to);
		if(dist < 0.001f)
			return to;
		return prop;
	}

	public static float clampedLerp(float frm, float to, float speed){
		float prop = Mathf.Lerp(frm, to, speed);
		float dist = Mathf.Abs(prop - to);
		if(dist < 0.001f)
			return to;
		return prop;
	}

	public static float LB2V(Vector3 a, Vector3 b){

		return Vector3.Magnitude(a - b);

	}


}
