using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class InRoomChatForUGUI : Photon.MonoBehaviour {

	public GameObject ChatWindowObject;
	public GameObject TeamSelectWindow;
	public InputField inputField;
	public CursorSetting cursorSetting;

	public string chatString;

	public static readonly string ChatRPC = "Chat";
	public List<string> messages = new List<string>();

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) && !TeamSelectWindow.activeSelf){
			if(!ChatWindowObject.activeSelf){
				ChatWindowObject.SetActive(true);
				inputField.ActivateInputField();
				cursorSetting.ChatLock();
			}else if(ChatWindowObject.activeSelf){
				if(inputField.text != ""){
					this.photonView.RPC("Chat", PhotonTargets.All, inputField.text);
					inputField.text = "";
				}
				cursorSetting.ChatUnlock();
				ChatWindowObject.SetActive(false);
			}
		}
		chatString = inputField.text;
	}

	[RPC]
	public void Chat(string newLine, PhotonMessageInfo mi)
	{
		string senderName = "anonymous";
		
		if (mi != null && mi.sender != null)
		{
			if (!string.IsNullOrEmpty(mi.sender.name))
			{
				senderName = mi.sender.name;
			}
			else
			{
				senderName = "player " + mi.sender.ID;
			}
		}
		
		this.messages.Add(senderName +": " + newLine);
	}

	public void AddLine(string newLine)
	{
		this.messages.Add(newLine);
	}
}
