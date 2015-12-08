using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TeamViewrStackManager : MonoBehaviour {
	[SerializeField]
	private GameObject namePlate;
	[SerializeField]
	private Sprite[] Icons = new Sprite[3];
	[SerializeField]
	private GameObject[] teamPanel = new GameObject[2];
	[SerializeField]
	private ArrayList[] nameList = new ArrayList[2]{new ArrayList(), new ArrayList()};
	// Use this for initialization
	public void AddPlayer(int teamColor, GameObject playerObject){
		nameList[teamColor].Add(playerObject);
	}

	public void RemovePlayer(int teamColor, GameObject playerObject){
		nameList[teamColor].Remove(playerObject);
	}

	public void RemoveAllPlayer(){
		for(int i = 0; i < 2; i++)
			nameList[i].Clear();
	}

	public IEnumerator RefreshViewer(){

		GameObject tempPlate;
		for(int teamColor = 0; teamColor < 2; teamColor++){
			DestroyAllNamePlates(teamColor);
			if(teamPanel[teamColor] == null){
				Debug.LogError((teamColor == 0 ? "Red" : "Blue") + "is missing.");
			}
			yield return new WaitForSeconds(0.1f);
			for(int i = 0; i < nameList[teamColor].Count; i++){
				Debug.Log((GameObject)nameList[teamColor].ToArray()[i]);
				tempPlate = MakeNamePlate((GameObject)nameList[teamColor].ToArray()[i]);
				Debug.Log("run");
				InstantiateNamePlate(tempPlate, teamColor, 0.87f - (0.13f * i), 0.98f - (0.13f * i));
			}
		}
	}

	private void DestroyAllNamePlates(int teamColor){
		for(int i = 0; i < teamPanel[teamColor].transform.childCount; i++){
			Destroy(teamPanel[teamColor].transform.GetChild(i).gameObject);
		}
	}

	private GameObject MakeNamePlate(GameObject playerObject){
		GameObject returnNamePlate = namePlate;
		Text nameText = returnNamePlate.transform.FindChild("Text").GetComponent<Text>();
		Image Icon = returnNamePlate.transform.FindChild("Image").GetComponent<Image>();
		Prophunt.SDUnitychan.Status.PlayerStatusManager playerStatusManager = playerObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
		nameText.text = playerStatusManager.playerName;
		Icon.sprite = Icons[playerStatusManager.charNum];
		return returnNamePlate;
	}

	private void InstantiateNamePlate(GameObject namePlate, int teamColor, float ancMin, float ancMax){
		GameObject temp = Instantiate(namePlate);
		//if(teamColor == 0)Debug.Log ("RED");
		//else Debug.Log("BLUE");
		RectTransform rectTransform = temp.GetComponent<RectTransform>();
		temp.transform.SetParent(teamPanel[teamColor].transform);
		rectTransform.anchorMin = new Vector2(0.02f, ancMin);
		rectTransform.anchorMax = new Vector2(0.98f, ancMax);
		rectTransform.offsetMin = new Vector2(0,0);
		rectTransform.offsetMax = new Vector2(0,0);
		rectTransform.localScale = new Vector3(1,1,1);
	}
}
