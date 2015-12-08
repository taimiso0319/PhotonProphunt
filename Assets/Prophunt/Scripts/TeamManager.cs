using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class TeamManager : Photon.MonoBehaviour {
	// Use this for initialization

	const int RED = 0;
	const int BLUE = 1;
	
	private Room room;
	private GameObject playerChildObject;
	private GameObject playerChildCameraRig;
	[SerializeField]
	private TeamViewrStackManager teamViewStackManager;

	[Header("0 = RED 1 = BLUE")]
	public bool editTest = false;
	[Header("Player Character")]
	public Transform[] playerPrefab;
	[Header("Player Spawn Point")]
	public GameObject[] spawnPoint = new GameObject[2];
	[Space(10)]
	public int[] countPlayer = new int[2];
	public int playerCount = 0;
	public int connectedPlayerCount = 0;
	[Header("GameObject List of Players")]
	public GameObject[] allPlayerList;
	public GameObject[] redPlayerList;
	public GameObject[] bluePlayerList;
	[Header("For Select Character UI")]
	public int charNum = 0;
	[SerializeField]
	private Toggle[] toggleButton;
	public bool isRefreshed = false;

	public void Update(){
		for(int i = 0; i < toggleButton.Length; i++){
			if(toggleButton[i].isOn)charNum = i;
		}
		connectedPlayerCount = PhotonNetwork.playerList.Length;
		if(connectedPlayerCount != playerCount){
			if(isRefreshed) isRefreshed = false;
			//if(playerCount != allPlayerList.Length){
			allPlayerList = GetAllPlayer();
			redPlayerList  = GetTeamPlayer(RED,allPlayerList);
			bluePlayerList = GetTeamPlayer(BLUE,allPlayerList);
			countPlayer[RED]  = redPlayerList.Length;
			countPlayer[BLUE] = bluePlayerList.Length;
			playerCount = allPlayerList.Length;
		}
		if(playerCount == connectedPlayerCount && !isRefreshed){
			isRefreshed = true;
			allPlayerList = GetAllPlayer();
			redPlayerList  = GetTeamPlayer(RED,allPlayerList);
			bluePlayerList = GetTeamPlayer(BLUE,allPlayerList);
			countPlayer[RED]  = redPlayerList.Length;
			countPlayer[BLUE] = bluePlayerList.Length;
			playerCount = allPlayerList.Length;
			if(teamViewStackManager != null){
				teamViewStackManager.RemoveAllPlayer();
				for(int i = 0; i < countPlayer[RED]; i++){
					teamViewStackManager.AddPlayer(RED, redPlayerList[i].transform.FindChild("Character").gameObject);
				}
				for(int i = 0; i < countPlayer[BLUE]; i++){
					teamViewStackManager.AddPlayer(BLUE, bluePlayerList[i].transform.FindChild("Character").gameObject);
				}
			}
			Debug.Log("###### red is " + countPlayer[RED] + "blue is " + countPlayer[BLUE] + " ######");
			StartCoroutine(teamViewStackManager.RefreshViewer());
		}
	}

	GameObject[] GetAllPlayer(){
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
		return playerList;
	}
	
	GameObject[] GetTeamPlayer(int color,GameObject[] allPlayer){
		List<GameObject> playerList = new List<GameObject>();
		GameObject playerChildObject;
		for(int i = 0; i < allPlayer.Length; i++){
			playerChildObject = allPlayer[i].transform.FindChild("Character").gameObject;
			if(playerChildObject.gameObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>().teamColor == color){
				playerList.Add(allPlayer[i]);
			}
		}
		return playerList.ToArray(); 
	}
	public void InstantiatePlayer(int teamColor){
		Debug.Log("Instantiate Player");
		GameObject playerObj = PhotonNetwork.Instantiate(this.playerPrefab[charNum].name, transform.position, Quaternion.Euler(0,1,0), 0) as GameObject;
		playerChildObject = playerObj.transform.FindChild("Character").gameObject;
		Prophunt.SDUnitychan.Status.PlayerStatusManager statusManager = playerChildObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
		if(photonView.isMine)statusManager.playerName = PhotonNetwork.player.name;
		statusManager.teamColor = teamColor;
		playerChildCameraRig = playerObj.transform.FindChild("Cameras").transform.FindChild("FreeLookCameraRig").gameObject;

		if(spawnPoint[RED] != null && spawnPoint[BLUE] != null){
			playerChildObject.transform.position = spawnPoint[teamColor].transform.position;
			playerChildCameraRig.transform.position = playerChildObject.transform.position;
			playerChildObject.transform.rotation = spawnPoint[teamColor].transform.rotation;
			playerChildCameraRig.transform.rotation = playerChildObject.transform.rotation;
		}else{
			Debug.Log("please set spawn points");
		}
		playerObj.name = PhotonNetwork.playerName;


	}

	public void RandomJoin(){
		int randNum = Random.Range(0,2);
		InstantiatePlayer(randNum);
	}
}
