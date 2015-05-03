using UnityEngine;
using System.Collections;

public class TeamManager : Photon.MonoBehaviour {
	// Use this for initialization
	public int redCount;
	public int blueCount;
	public int playerCount;
	public bool editTest = false;
	Room room;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddPlayer(GameObject playerObj){
		int teamNum = 0;
		room = PhotonNetwork.room;
		playerCount = room.playerCount;
		if(playerCount%2 == 0)teamNum = 0;
		else if(playerCount%2 == 1)teamNum = 1;
		playerObj.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>().teamNum = teamNum;
	}

	public void RemovePlayer(GameObject playerObj){

	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(!editTest){
			if (stream.isWriting)
			{
				// We own this player: send the others our data
				//nameText.text = playerName;
				stream.SendNext(playerCount);
				stream.SendNext(redCount);
				stream.SendNext(blueCount);
			}
			else
			{
				// Network player, receive data
				this.playerCount = (int)stream.ReceiveNext();
				this.redCount = (int)stream.ReceiveNext();
				this.blueCount = (int)stream.ReceiveNext();
				//nameText.text = playerName;
			}
		}
	}
}
