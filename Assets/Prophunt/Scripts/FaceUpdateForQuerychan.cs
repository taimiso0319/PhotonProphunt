using UnityEngine;
using System.Collections;

public class FaceUpdateForQuerychan : MonoBehaviour {

	[SerializeField]
	private Material[] QuerychanFaceMaterial;

	[SerializeField]
	private GameObject Face;

	public enum QuerychanFaceType{
		DEFAULT 	= 0,
		SMILE 		= 1,
		ANGER		= 2,
		SURPRIZE	= 3,
		SAD			= 4,
		DRUNK		= 5,
	};

	public void ChangeFace (QuerychanFaceType faceNum)
	{
		Face.GetComponent<Renderer>().material = QuerychanFaceMaterial[(int)faceNum];
	}
}
