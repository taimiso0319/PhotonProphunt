using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prophunt.SDUnitychan.Status{
	public class PlayerStatusManager : Photon.MonoBehaviour {
		public bool isMine = false;
		public float hitPoint = 100;
		public int teamNum;
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
		
		}
		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
			if(stream.isWriting){
				stream.SendNext(hitPoint);
				stream.SendNext(teamNum);
			}else{
				hitPoint = (float)stream.ReceiveNext();
				teamNum = (int)stream.ReceiveNext();
			}
		}
	}
}
