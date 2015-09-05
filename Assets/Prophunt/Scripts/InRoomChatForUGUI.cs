using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class InRoomChatForUGUI : Photon.MonoBehaviour {

	[SerializeField]
	private GameObject ChatWindowObject;
	[SerializeField]
	private GameObject TeamSelectWindow;
	[SerializeField]
	private InputField inputField;
	[SerializeField]
	private CursorSetting cursorSetting;
	[SerializeField]
	private RectTransform chatPanelRect;
	[SerializeField]
	private Text chatText;
	[SerializeField]
	private Scrollbar verticalScrollbar;

	//private bool inputing = false;

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
				inputField.text = inputField.text.Replace(System.Environment.NewLine, "");
				if(inputField.text != ""){
					this.photonView.RPC("Chat", PhotonTargets.All, inputField.text);
					inputField.text = "";
					inputField.ActivateInputField();
				}else if(inputField.text == ""){
					cursorSetting.ChatUnlock();
					ChatWindowObject.SetActive(false);
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape)&&ChatWindowObject.activeSelf){
			cursorSetting.ChatUnlock();
			ChatWindowObject.SetActive(false);
		}else if(Input.GetKeyDown(KeyCode.Escape)&&!ChatWindowObject.activeSelf){
			ChatWindowObject.SetActive(true);
			cursorSetting.ChatLock();
		}
		if(messages.Count != 0){
			chatText.text = "";
			for(int i = 0; i < messages.Count; i++){
				chatText.text += messages[i] + System.Environment.NewLine;
			}
		}
	}

	[PunRPC]
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
		if(this.messages.Count >= 8){
			chatPanelRect.offsetMax += new Vector2(0,24);
			verticalScrollbar.value = 0;
		}
	}

	public void AddLine(string newLine)
	{
		this.messages.Add(newLine);
	}
}
