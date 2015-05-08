// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerInGame.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class WorkerInGame : Photon.MonoBehaviour
{
    public Transform playerPrefab;
	public TeamManager teamManager;
	public GameObject redSpawnPoint;
	public GameObject blueSpawnPoint;
	private GameObject playerChildObject;
	private GameObject playerChildCameraRig;
    public void Awake()
    {
		if(teamManager==null)teamManager = GetComponent<TeamManager>();
        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.connected)
        {
            Application.LoadLevel(WorkerMenu.SceneNameMenu);
            return;
        }
        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		GameObject playerObj = PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0) as GameObject;
		playerChildObject = playerObj.transform.FindChild("SDUnitychan").gameObject;
		playerChildCameraRig = playerObj.transform.FindChild("Cameras").transform.FindChild("FreeLookCameraRig").gameObject;
		teamManager.AddPlayer(playerChildObject);
		Prophunt.SDUnitychan.Status.PlayerStatusManager statusManager = playerChildObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
		if(redSpawnPoint != null && blueSpawnPoint != null && statusManager.teamNum == 0){
			playerChildObject.transform.position = redSpawnPoint.transform.position;
			playerChildCameraRig.transform.position = playerChildObject.transform.position;
			playerChildObject.transform.rotation = redSpawnPoint.transform.rotation;
		}else if(redSpawnPoint != null && blueSpawnPoint != null && statusManager.teamNum == 1){
			playerChildObject.transform.position = blueSpawnPoint.transform.position;
			playerChildCameraRig.transform.position = playerChildObject.transform.position;
			playerChildObject.transform.rotation = blueSpawnPoint.transform.rotation;
		}else{
			Debug.Log("please set spawn points");
		}
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Return to Lobby"))
        {
            PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
        }
    }

    public void OnMasterClientSwitched(PhotonPlayer player)
    {
        Debug.Log("OnMasterClientSwitched: " + player);

        string message;
        InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message

        if (chatComponent != null)
        {
            // to check if this client is the new master...
            if (player.isLocal)
            {
                message = "You are Master Client now.";
            }
            else
            {
                message = player.name + " is Master Client now.";
            }


            chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
        }
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom (local)");
        
        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton");

        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		teamManager.playerCount++;
        Debug.Log("OnPhotonPlayerConnected: " + player);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
		teamManager.playerCount--;
        Debug.Log("OnPlayerDisconneced: " + player);
    }

    public void OnFailedToConnectToPhoton()
    {
        Debug.Log("OnFailedToConnectToPhoton");

        // back to main menu        
        Application.LoadLevel(WorkerMenu.SceneNameMenu);
    }
}
