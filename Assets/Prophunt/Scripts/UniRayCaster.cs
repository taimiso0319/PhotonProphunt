﻿using UnityEngine;
using Photon;
using System.Collections;

public class UniRayCaster : Photon.MonoBehaviour {
	float timer = 0;
	bool countStart = false;
	void Update(){
		if(Physics.Raycast(transform.position, Vector3.down, transform.localScale.y/2 + 0.01f)){
			countStart = true;
		}
	}
	void FixedUpdate(){
		if(countStart)timer += Time.deltaTime;
		if(timer > 3) PhotonNetwork.Destroy(this.gameObject);
	}
}
