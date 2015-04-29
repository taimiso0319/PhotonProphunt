using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameSync : Photon.MonoBehaviour {

	public GameObject canvasObj;
	public bool editTest = false;
	public string playerName;
	private Image namePlate;
	private Text nameText;
	public Prophunt.SDUnitychan.Status.PlayerStatusManager playerStatusManager;

	// Use this for initialization
	void Start () {
		nameText = GetComponent<Text>();
		namePlate = canvasObj.transform.FindChild("Panel").GetComponent<Image>();
		nameText.text = null;
		playerName = PhotonNetwork.player.name;
	}

	void LateUpdate(){
		nameText.text = playerName;
		if(playerStatusManager.isMine && namePlate.color.a != 1){
			namePlate.color = new Color32(0,0,0,1);
			nameText.color = new Color32(0,0,0,1);
		}
		if(playerStatusManager.teamNum == 0 && !playerStatusManager.isMine)namePlate.color = new Color32(255,128,128,45);
		else if(playerStatusManager.teamNum == 1 && !playerStatusManager.isMine)namePlate.color = new Color32(128,128,255,45);

	}
	// Update is called once per frame

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(!editTest){
			if (stream.isWriting)
			{
				// We own this player: send the others our data
				//nameText.text = playerName;
				stream.SendNext(playerName);
			}
			else
			{
				// Network player, receive data
				this.playerName = (string)stream.ReceiveNext();
				//nameText.text = playerName;
			}
		}
	}
}
