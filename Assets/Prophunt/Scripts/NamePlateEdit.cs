using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NamePlateEdit : Photon.MonoBehaviour {
	
	const int RED = 0;
	const int BLUE = 1;

	public GameObject canvasObj;
	public bool editTest = false;
	public string playerName;
	private Image namePlate;
	private Text nameText;
	public Prophunt.SDUnitychan.Status.PlayerStatusManager playerStatusManager;

	// Use this for initialization
	void Start () {
		nameText = canvasObj.transform.GetComponentInChildren<Text>();
		namePlate = canvasObj.transform.GetComponentInChildren<Image>();
		nameText.text = null;
	}

	void LateUpdate(){
		nameText.text = playerStatusManager.playerName;
		if(playerStatusManager.isMine && namePlate.color.a != 1){
			namePlate.color = new Color32(0,0,0,1);
			nameText.color = new Color32(0,0,0,1);
		}
		if(playerStatusManager.teamColor == RED && !playerStatusManager.isMine)namePlate.color = new Color32(255,128,128,45);
		else if(playerStatusManager.teamColor == BLUE && !playerStatusManager.isMine)namePlate.color = new Color32(128,128,255,45);

	}
}
