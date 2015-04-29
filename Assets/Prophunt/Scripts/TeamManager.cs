using UnityEngine;
using System.Collections;

public class TeamManager : Photon.MonoBehaviour {
	// Use this for initialization
	public int team1Count;
	public int team2Count;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddPlayer(GameObject playerObj){
		int teamNum = PhotonNetwork.countOfPlayersInRooms%2;
		if(team1Count == team2Count&&teamNum == 0)team1Count++;
		else if(team1Count != team2Count && team1Count > team2Count && teamNum == 0){teamNum = 1; team2Count++;}
		else if(team1Count != team2Count && team1Count < team2Count && teamNum == 1){teamNum = 0; team1Count++;}
		playerObj.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>().teamNum = teamNum;
	}
}
