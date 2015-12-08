using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCountText : MonoBehaviour {
	const int RED = 0;
	const int BLUE = 1;
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
		redCounterText.text = "Red Player: " + teamManager.countPlayer[RED].ToString();
		blueCounterText.text = "Blue Player: " + teamManager.countPlayer[BLUE].ToString();
		if(teamManager.countPlayer[RED] < teamManager.countPlayer[BLUE]){
			blueJoinButton.interactable = false;
			redJoinButton.interactable = true;
			randomJoinButton.interactable = false;
		}
		if(teamManager.countPlayer[BLUE] < teamManager.countPlayer[RED]){
			redJoinButton.interactable = false;
			blueJoinButton.interactable = true;
			randomJoinButton.interactable = false;
		}
		if(teamManager.countPlayer[RED] == teamManager.countPlayer[BLUE]){
			redJoinButton.interactable = true;
			blueJoinButton.interactable = true;
			randomJoinButton.interactable = true;
		}
	}
}
