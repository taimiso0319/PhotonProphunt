using UnityEngine;
using System.Collections;

public class CanvasActiveChange : MonoBehaviour {

	public void Activate(){
		this.gameObject.SetActive(true);
	}
	public void Deactivate(){
		this.gameObject.SetActive(false);
	}
}
