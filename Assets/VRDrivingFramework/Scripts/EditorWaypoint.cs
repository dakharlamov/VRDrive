//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorWaypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnValidate () {
	
		RaycastHit hit;
		if(Physics.Raycast(this.transform.position, Vector3.down, out hit)){

			this.transform.position = hit.point;

		}

	}


	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(this.transform.position, 0.5f);

	}
}
