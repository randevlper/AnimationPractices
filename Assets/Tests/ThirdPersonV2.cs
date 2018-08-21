using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonV2 : MonoBehaviour {
	public float turnSpeed = 10f;
	public KeyCode sprintJoystick = KeyCode.JoystickButton2;
	public KeyCode sprintKeyboard = KeyCode.LeftShift;
	public float speedMult = 1.0f;
	private float turnSpeedMultiplier;
	private float forwardVelocity = 0f;
	private float direction = 0f;
	private bool isSprinting = false;
	private Animator anim;
	private Vector3 targetDirection;
	private Vector2 input;
	private Quaternion freeRotation;
	private Camera mainCamera;
	//private float forwardVelocity;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		anim = GetComponent<Animator> ();
		mainCamera = Camera.main;
	}

	private void LateUpdate() {
		transform.position += (anim.deltaPosition);
	}

	// Update is called once per frame
	void FixedUpdate () {

		input.x = Input.GetAxis ("Horizontal");
		input.y = Input.GetAxis ("Vertical");

		// set speed to both vertical and horizontal inputs
		forwardVelocity = Mathf.Abs (input.x) + Mathf.Abs (input.y);

		forwardVelocity = Mathf.Clamp (forwardVelocity, 0f, 1f);
		//speed = Mathf.SmoothDamp (anim.GetFloat ("Speed"), speed, ref velocity, 0.1f);
		anim.SetFloat ("Speed", forwardVelocity);

		//if (input.y < 0f && keepDirection) direction = input.y;
		direction = 0f;
		anim.SetFloat ("Direction", direction);

		// set sprinting
		if ((Input.GetKeyDown (sprintJoystick) || Input.GetKeyDown (sprintKeyboard)) && input != Vector2.zero && direction >= 0f) isSprinting = true;
		if ((Input.GetKeyUp (sprintJoystick) || Input.GetKeyUp (sprintKeyboard)) || input == Vector2.zero) isSprinting = false;
		anim.SetBool ("isSprinting", isSprinting);

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