using UnityEngine;
using System.Collections;

public class CursorSetting : MonoBehaviour {
	public bool isLobby = false;
	public bool isCursorLocked = true;
	void Start(){
		//if(!isLobby)CursorLock();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.T)){
			if(isCursorLocked){
				CursorUnLock();
			}else if(!isCursorLocked&&!isLobby){
				CursorLock();
			}
		}
	}
	
	public void CursorLock(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		isCursorLocked = true;
	}
	
	public void CursorUnLock(){
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		isCursorLocked = false;
	}
}
