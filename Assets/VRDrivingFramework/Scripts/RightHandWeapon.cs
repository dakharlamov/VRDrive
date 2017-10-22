//Daniel Kharlamov

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandWeapon : MonoBehaviour {

	public InputController inputController;

	private bool gunEnabled;

	private bool hidden;

	private bool fireRateLock;

	public float fireRate = 0.25f;

	// Use this for initialization
	void Start () {

		gunEnabled = inputController.getAButtonToggleState();
		hidden = true;
		foreach(MeshRenderer rend in this.GetComponentsInChildren<MeshRenderer>()){
			rend.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		gunEnabled = inputController.getAButtonToggleState();

		updateGunState();

		if(gunEnabled && !fireRateLock){

			float trigger = inputController.getRightTriggerSqueeze();

			Debug.DrawRay(this.transform.position, -1.0f * this.transform.right);

			if(trigger > 0.5f){
				this.GetComponentInChildren<ParticleSystem>().Emit(1);

				RaycastHit hit;

				if(Physics.Raycast(this.transform.position, -1.0f * this.transform.right, out hit)){

					//GameObject.Find("DBA").GetComponent<TextMesh>().text = hit.collider.tag;

					if(hit.collider.tag == "enemyCar"){

						hit.collider.gameObject.GetComponentInParent<CarAIWaypoint>().damageCar();

					}

				}

				fireRateLock = true;

				Invoke("fireRateUnlock", fireRate);
			}
		}




	}

	private void fireRateUnlock(){

		fireRateLock = false;

	}


	private void updateGunState(){


		if(gunEnabled){
			if(hidden){
				foreach(MeshRenderer rend in this.GetComponentsInChildren<MeshRenderer>()){
					rend.enabled = true;
				}
				hidden = false;	
			}

			inputController.lockAcceleration();


		}else{
			if(!hidden){
				foreach(MeshRenderer rend in this.GetComponentsInChildren<MeshRenderer>()){
					rend.enabled = false;
				}
				hidden = true;
			}

			inputController.unlockAcceleration();


		}



	}




}
