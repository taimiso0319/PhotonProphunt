using UnityEngine;
using System.Collections;

public class DestroyArea : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Bullet")){
			PhotonNetwork.Destroy(col.gameObject);
		}
		if(col.CompareTag("Player")){
			col.transform.position = new Vector3(0,3,0);
		}
	}
}
