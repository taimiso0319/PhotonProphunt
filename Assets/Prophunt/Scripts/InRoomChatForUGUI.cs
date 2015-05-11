using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InRoomChatForUGUI : MonoBehaviour {

	public GameObject ChatWindowObject;
	public GameObject TeamSelectWindow;
	public InputField inputField;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) && !TeamSelectWindow.activeSelf){
			if(!ChatWindowObject.activeSelf){
				ChatWindowObject.SetActive(true);
			}else if(ChatWindowObject.activeSelf){
				ChatWindowObject.SetActive(false);
			}
		}
	}
}
