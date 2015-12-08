using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prophunt.SDUnitychan.Status{
	public class PlayerStatusManager : Photon.MonoBehaviour {
		const int RED = 0;
		const int BLUE = 1;
		public bool isMine = false;
		public float hitPoint = 100;
		public int teamColor;
		public string playerName;
		public int charNum = 0;

		public void Update(){
			if(photonView.isMine)playerName = PhotonNetwork.player.name;
			if(!photonView.isMine)this.gameObject.transform.parent.name = playerName;
		}

		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
			if(stream.isWriting){
				stream.SendNext(hitPoint);
				stream.SendNext(teamColor);
				playerName = PhotonNetwork.player.name;
				stream.SendNext(playerName);
			}else{
				hitPoint = (float)stream.ReceiveNext();
				teamColor = (int)stream.ReceiveNext();
				playerName = (string)stream.ReceiveNext();
			}
		}
	}
}
