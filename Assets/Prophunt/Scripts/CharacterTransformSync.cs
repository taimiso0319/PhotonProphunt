using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PhotonView))]
public class CharacterTransformSync : Photon.MonoBehaviour {
	
	private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private string correctAnimName = "";
	private string sendAnimName = "Standing@loop";
	public GameObject rootObject;

	public bool editTest = false;

	public GameObject charObject;
	public GameObject camerasObject;
	private Animator charObjectAnim;
	private Prophunt.SDUnitychan.ThirdPerson.ThirdPersonCharacter thirdpersonCharacter;
	private Prophunt.SDUnitychan.Status.PlayerStatusManager playerStatus;

	void Awake(){
		if(!editTest){
			if(rootObject!=null){
			}else{
				rootObject = this.gameObject;
				Debug.Log("no charObject in script");
			}
			if(charObject==null)charObject = (GameObject)rootObject.transform.FindChild("SDUnitychan").gameObject;
			if(camerasObject==null)camerasObject = rootObject.transform.FindChild("Cameras").gameObject;
			charObjectAnim = (Animator)charObject.GetComponent<Animator>();
			thirdpersonCharacter = charObject.GetComponent<Prophunt.SDUnitychan.ThirdPerson.ThirdPersonCharacter>();
			playerStatus = charObject.GetComponent<Prophunt.SDUnitychan.Status.PlayerStatusManager>();
			if(photonView.isMine){
				Debug.Log("isMine");
				playerStatus.isMine = true;
			}else{
				camerasObject.SetActive(false);
				charObject.GetComponent<Prophunt.SDUnitychan.ThirdPerson.ThirdPersonUserControl>().isControllable = false;
				Debug.Log("isNotMine");
				playerStatus.isMine = false;
			}
		}
	}

	void Start(){
		PhotonNetwork.sendRate = 30;
		PhotonNetwork.sendRateOnSerialize = 30;
	}

	// Update is called once per frame
	void Update()
	{
		if(!editTest){
			if (!photonView.isMine)
			{
				charObject.transform.localPosition = Vector3.Lerp(charObject.transform.localPosition, this.correctPlayerPos, Time.deltaTime * 5);
				charObject.transform.localRotation = Quaternion.Lerp(charObject.transform.localRotation, this.correctPlayerRot, Time.deltaTime * 5);
				if(correctAnimName == "Running@loop")charObjectAnim.SetFloat("MoveSpeed",1.0f);
				if(correctAnimName == "Walking@loop")charObjectAnim.SetFloat("MoveSpeed",0.5f);
				if(correctAnimName == "Jumping@loop")thirdpersonCharacter.HandleGroundedMovement(false,true);
				else thirdpersonCharacter.HandleGroundedMovement(false,false);
				if(correctAnimName == "Standing@loop")charObjectAnim.SetFloat("MoveSpeed",0.0f);
			}
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(!editTest){
			if(charObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Standing@loop"))sendAnimName = "Standing@loop";
			if(charObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Walking@loop"))sendAnimName = "Walking@loop";
			if(charObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Running@loop"))sendAnimName = "Running@loop";
			if(charObjectAnim.GetCurrentAnimatorStateInfo(0).IsName("Jumping@loop"))sendAnimName = "Jumping@loop";
			if (stream.isWriting)
			{
				// We own this player: send the others our data
				stream.SendNext(charObject.transform.localPosition);
				stream.SendNext(charObject.transform.localRotation);
				stream.SendNext(sendAnimName);
			}
			else
			{
				// Network player, receive data
				this.correctPlayerPos = (Vector3)stream.ReceiveNext();
				this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
				this.correctAnimName = (string)stream.ReceiveNext();
			}
		}
	}

}
