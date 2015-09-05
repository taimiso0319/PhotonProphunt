using UnityEngine;
using System.Collections;

public class RuleManager : MonoBehaviour {

	public bool FreePlay = true;
	public bool BallToss = false;

	public void Update(){
		if(FreePlay){
			BallToss = false;
		}
	}

}
