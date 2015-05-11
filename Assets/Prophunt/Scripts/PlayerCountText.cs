using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCountText : MonoBehaviour {

	public GameObject managerObject;
	public Button redJoinButton;
	public Button blueJoinButton;
	public Button randomJoinButton;
	public Text redCounterText;
	public Text blueCounterText;
	private TeamManager teamManager;
	// Use this for initialization
	void Start () {
		teamManager = managerObject.GetComponent<TeamManager>();
	}
	
	// Update is called once per frame
	void Update () {
		redCounterText.text = "Red Player: " + teamManager.countRedPlayer.ToString();
		blueCounterText.text = "Blue Player: " + teamManager.countBluePlayer.ToString();
		if(teamManager.countRedPlayer < teamManager.countBluePlayer){
			blueJoinButton.interactable = false;
			redJoinButton.interactable = true;
			randomJoinButton.interactable = false;
		}
		if(teamManager.countBluePlayer < teamManager.countRedPlayer){
			redJoinButton.interactable = false;
			blueJoinButton.interactable = true;
			randomJoinButton.interactable = false;
		}
		if(teamManager.countRedPlayer == teamManager.countBluePlayer){
			redJoinButton.interactable = true;
			blueJoinButton.interactable = true;
			randomJoinButton.interactable = true;
		}
	}
}
