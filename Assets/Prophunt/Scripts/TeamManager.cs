using UnityEngine;
using System.Collections;

public class TeamManager : Photon.MonoBehaviour {
	// Use this for initialization
	public bool editTest = false;
	public Transform playerPrefab;
	public GameObject redSpawnPoint;
	public GameObject blueSpawnPoint;
	private GameObject playerChildObject;
	private GameObject playerChildCameraRig;
	public int countRedPlayer = 0;
	public int countBluePlayer = 0;
	Room room;

	public void Update(){
		int redCount = 0;
		int blueCount = 0;
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in playerList){
			Prophunt.SDUnitychan.Status.PlayerStatusManager statusManager = player.transform.FindChild("Character").gameObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
			if(statusManager.teamNum == 0)redCount++;
			else if(statusManager.teamNum == 1)blueCount++;
		}
		countRedPlayer = redCount;
		countBluePlayer = blueCount;
	}

	public void InstantiatePlayer(int teamNum){
		GameObject playerObj = PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.Euler(0,1,0), 0) as GameObject;
		playerChildObject = playerObj.transform.FindChild("Character").gameObject;
		playerChildCameraRig = playerObj.transform.FindChild("Cameras").transform.FindChild("FreeLookCameraRig").gameObject;
		Prophunt.SDUnitychan.Status.PlayerStatusManager statusManager = playerChildObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
		statusManager.teamNum = teamNum;
		if(redSpawnPoint != null && blueSpawnPoint != null && teamNum == 0){
			playerChildObject.transform.position = redSpawnPoint.transform.position;
			playerChildCameraRig.transform.position = playerChildObject.transform.position;
			playerChildObject.transform.rotation = redSpawnPoint.transform.rotation;
			playerChildCameraRig.transform.rotation = playerChildObject.transform.rotation;
		}else if(redSpawnPoint != null && blueSpawnPoint != null && teamNum == 1){
			playerChildObject.transform.position = blueSpawnPoint.transform.position;
			playerChildCameraRig.transform.position = playerChildObject.transform.position;
			playerChildObject.transform.rotation = blueSpawnPoint.transform.rotation;
			playerChildCameraRig.transform.rotation = playerChildObject.transform.rotation;
		}else{
			Debug.Log("please set spawn points");
		}
	}

	public void RandomJoin(){
		int randNum = Random.Range(0,2);
		InstantiatePlayer(randNum);
	}
}
