using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonV2 : MonoBehaviour {
	[SerializeField] private CharacterController characterController;
	private Camera mainCamera;
	private Animator anim;
	public float jumpForce = 10;
	public float friction = 0.1f;
	public float turnSpeed = 10f;
	public KeyCode sprintJoystick = KeyCode.JoystickButton2;
	public KeyCode sprintKeyboard = KeyCode.LeftShift;
	public float speedMult = 2.0f;
	private float turnSpeedMultiplier;
	private float forwardVelocity = 0f;
	private float direction = 0f;
	private bool isSprinting = false;
	private Vector3 targetDirection;
	private Vector2 input;
	private Quaternion freeRotation;
	//private float forwardVelocity;
	[SerializeField] private Vector3 velocity;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		anim = GetComponent<Animator> ();
		mainCamera = Camera.main;
	}

	private void Update () {

		if(IsUnlockButtonPressed()){
			Cursor.lockState = CursorLockMode.None;
			return;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
		}

		if (characterController.isGrounded) {
			velocity.y = 0;
		}
		if (Input.GetButtonDown ("Jump") && characterController.isGrounded) {
			anim.SetBool ("Jump", true);
			velocity.y = jumpForce;
		} else {
			anim.SetBool ("Jump", false);
		}
		anim.SetFloat ("Vertical", velocity.y);
	}

	private void LateUpdate () {

		//transform.position += (anim.deltaPosition);
		//characterController.Move (anim.deltaPosition);

	}

	public bool IsUnlockButtonPressed(){
		if(Input.GetKey(KeyCode.LeftControl)){
			Cursor.lockState = CursorLockMode.None;
			return true;
		}
		return false;
	}

	//Add Jumping
	//Add CharacterController
	void FixedUpdate () {

		if(IsUnlockButtonPressed()) {return;}

		input.x = Input.GetAxis ("Horizontal");
		input.y = Input.GetAxis ("Vertical");

		velocity += Physics.gravity * Time.deltaTime;

		// set speed to both vertical and horizontal inputs
		forwardVelocity = Mathf.Abs (input.x) + Mathf.Abs (input.y);
		forwardVelocity = Mathf.Clamp (forwardVelocity, 0f, 1f);
		//speed = Mathf.SmoothDamp (anim.GetFloat ("Speed"), speed, ref velocity, 0.1f);
		anim.SetFloat ("Speed", forwardVelocity);

		direction = 0f;
		anim.SetFloat ("Direction", direction);

		// set sprinting
		//if ((Input.GetKeyDown (sprintJoystick) || Input.GetKeyDown (sprintKeyboard)) && input != Vector2.zero && direction >= 0f) isSprinting = true;
		//if ((Input.GetKeyUp (sprintJoystick) || Input.GetKeyUp (sprintKeyboard)) || input == Vector2.zero) isSprinting = false;
		//anim.SetBool ("isSprinting", isSprinting);
		if (Input.GetKey (sprintKeyboard)) {
			anim.SetFloat ("Sprint", speedMult);
		} else {
			anim.SetFloat ("Sprint", 1.0f);
		}

		// Update target direction relative to the camera view (or not if the Keep Direction option is checked)
		UpdateTargetDirection ();
		if (input != Vector2.zero && targetDirection.magnitude > 0.1f) {
			Vector3 lookDirection = targetDirection.normalized;
			freeRotation = Quaternion.LookRotation (lookDirection, transform.up);
			float diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
			float eulerY = transform.eulerAngles.y;

			if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
			Vector3 euler = new Vector3 (0, eulerY, 0);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
		}
		velocity += transform.forward * forwardVelocity * (Input.GetKey (sprintKeyboard) ? speedMult : 1f);
		velocity.x -= velocity.x * friction;
		velocity.z -= velocity.z * friction;

		characterController.Move (velocity * Time.deltaTime);
	}

	public virtual void UpdateTargetDirection () {

		turnSpeedMultiplier = 1f;
		var forward = mainCamera.transform.TransformDirection (Vector3.forward);
		forward.y = 0;

		//get the right-facing direction of the referenceTransform
		var right = mainCamera.transform.TransformDirection (Vector3.right);

		// determine the direction the player will face based on input and the referenceTransform's right and forward directions
		targetDirection = input.x * right + input.y * forward;

	}
}