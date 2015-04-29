using UnityEngine;
using System.Collections;

public class BulletTransformSync : Photon.MonoBehaviour {

	private Vector3 correctVector3;
	private Quaternion correctQuaternion;

	void FixedUpdate(){
		transform.position = correctVector3;
		transform.rotation = correctQuaternion;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo msgInfor){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}else{
			correctVector3 = 	(Vector3)stream.ReceiveNext();
			correctQuaternion = (Quaternion)stream.ReceiveNext();
		}
	}

}
