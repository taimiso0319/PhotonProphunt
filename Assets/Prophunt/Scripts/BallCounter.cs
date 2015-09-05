using UnityEngine;
using System.Collections;
using Photon;

public class BallCounter : Photon.MonoBehaviour {


	public int ballCount = 0;
	private int correctBallCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ballCount = ballCount;
	}

	void OnPhotonSerializeVire(PhotonStream stream){
		if(stream.isWriting){
			stream.SendNext(ballCount);
		}else if(stream.isReading){
			correctBallCount = (int)stream.ReceiveNext();
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Bullet"))ballCount++;
	}
}
