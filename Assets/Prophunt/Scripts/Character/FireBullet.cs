using UnityEngine;
using Photon;
using System.Collections;

public class FireBullet : Photon.MonoBehaviour {

	Prophunt.SDUnitychan.ThirdPerson.ThirdPersonUserControl tpsController;

	public GameObject BulletObject;
	public GameObject TargetPosition;
	public float firePower = 5.0f;

	public Vector3 targetVector;

	private float countdownSec = 2;
	public float waitSec = 2;
	private bool isShootable = true;

	// Use this for initialization
	void Awake(){
		tpsController = GetComponent<Prophunt.SDUnitychan.ThirdPerson.ThirdPersonUserControl>();
		PhotonNetwork.sendRate = 30;
		PhotonNetwork.sendRateOnSerialize = 30;
	}
	
	// Update is called once per frame
	void Update () {
		targetVector = (TargetPosition.transform.position - this.transform.position).normalized;
		if(Input.GetMouseButtonDown(0)){
			if(tpsController.isControllable&&tpsController.isCurosrLocked&&isShootable){
				BulletInstantiate(targetVector);
				isShootable = false;
			}
		}
		if(!isShootable){
			countdownSec -= Time.deltaTime;
			if(countdownSec <= 0)isShootable = true;
		}
		if(isShootable){
			countdownSec = waitSec;
		}
	}

	void BulletInstantiate(Vector3 addForceVector){
		GameObject localGameObject;
		Rigidbody localGameObjectRigidbody;
		if(this.transform.parent.FindChild("Bullets") == null){
			GameObject Bullets = new GameObject();
			Bullets.name = "Bullets";
			Bullets.transform.parent = this.transform.parent.transform;
		}
		localGameObject = (GameObject)PhotonNetwork.Instantiate(BulletObject.name,this.transform.position + (addForceVector * 1.05f) + (Vector3.up * 0.1f),TargetPosition.transform.rotation,0) as GameObject;
		localGameObjectRigidbody = localGameObject.GetComponent<Rigidbody>();
		localGameObjectRigidbody.transform.parent = this.transform.parent.FindChild("Bullets").gameObject.transform;
		localGameObjectRigidbody.AddForce(addForceVector * firePower);
	}
}
