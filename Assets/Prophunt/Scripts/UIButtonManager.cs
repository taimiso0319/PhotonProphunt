using UnityEngine;
using System.Collections;

public class UIButtonManager : MonoBehaviour {

	// This script script is for controlling UI
	// This works to apear UI or to hide UI
	// This requires to set to initialize variables

	[SerializeField]
	private GameObject TeamViewerUI;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(TeamViewerUI!=null)TeamViewerUI.SetActive(Input.GetKey(KeyCode.Tab));
	}
}
