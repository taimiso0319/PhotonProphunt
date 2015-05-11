using UnityEngine;
using System.Collections;

public static class TransformExtensions {

	public static void SetPosition(this Transform transform, float x, float y, float z){
		transform.position = new Vector3(x,y,z);
	}
}
