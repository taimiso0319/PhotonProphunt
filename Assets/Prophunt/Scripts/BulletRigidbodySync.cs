using UnityEngine;
using System.Collections;

public class BulletRigidbodySync : MonoBehaviour {

	Rigidbody rigidBody;

	void Start(){
		rigidBody = GetComponent<Rigidbody>();
		PhotonNetwork.sendRate = 30;
		PhotonNetwork.sendRateOnSerialize = 30;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo msgInfo){
		if(stream.isWriting){
			stream.SendNext(rigidBody.velocity);
			stream.SendNext(rigidBody.angularVelocity);
		}else{
			rigidBody.velocity = (Vector3)stream.ReceiveNext();
			rigidBody.angularVelocity = (Vector3)stream.ReceiveNext();
		}
	}
}
