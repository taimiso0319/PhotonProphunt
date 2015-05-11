using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Prophunt.SDUnitychan.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;     // the world-relative desired move direction, calculated from the camForward and user input.

		///test scripts

		//private Rigidbody rigBody;
		public float moveSpeed = 3.0f;
		public bool isDebug = false;
		private float vecSpeed;
		/// 
		CursorSetting cursorSetting;
		public bool isCurosrLocked = false;

		public bool isControllable = true;
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
			cursorSetting = GameObject.Find("Manager").GetComponent<CursorSetting>();
			//rigBody = GetComponent<Rigidbody>();
        }
		/*
        private void Update()
        {

            if (!m_Jump)
            {
                //m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
				m_Jump = Input.GetButtonDown("Jump");
			}

			//rigBody.position += transform.forward * Time.deltaTime;
		}
		*/

		private void Update(){
			if(isControllable&&isCurosrLocked){
				if(Input.GetButtonDown("Jump")){
					Debug.Log("Jump");
					m_Jump = true;
				}
			}
		}

        // Fixed update is called in sync with physics
        private void FixedUpdate()
		{
			isCurosrLocked = cursorSetting.isCursorLocked;
			if(isControllable&&isCurosrLocked){
				/*if(Input.GetButtonDown("Jump")){
					Debug.Log("Jump");
					m_Jump = true;
				}*/
	            // read inputs
	            float h = CrossPlatformInputManager.GetAxis("Horizontal");
	            float v = CrossPlatformInputManager.GetAxis("Vertical");
				bool crouch = Input.GetKey(KeyCode.C);
				// calculate move direction to pass to character
				vecSpeed = Mathf.Abs(h) + Mathf.Abs(v);
				if(vecSpeed > 1) vecSpeed = 1;
	            if (m_Cam != null)
	            {
	                // calculate camera relative direction to move:
	                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
	                m_Move = v*m_CamForward + h*m_Cam.right;
	            }
	            else
	            {
	                // we use world-relative directions in the case of no main camera
	                m_Move = v*Vector3.forward + h*Vector3.right;
	            }
#if !MOBILE_INPUT
				// walk speed multiplier
				if (Input.GetKey(KeyCode.LeftShift)){ m_Move *= 0.5f; vecSpeed *= 0.5f; }
#endif
				// move character with transform
				transform.position += transform.forward * vecSpeed * Time.deltaTime * moveSpeed;
				m_Character.SetMoveSpeed(vecSpeed);

	            // pass all parameters to the character control script
	            m_Character.Move(m_Move, crouch, m_Jump);
	            m_Jump = false;
			}
        }
    }
}
