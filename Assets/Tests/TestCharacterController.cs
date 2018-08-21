using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterController : MonoBehaviour {
	[SerializeField] CharacterController controller;
	[SerializeField] Animator animController;
	[SerializeField] Camera mainCamera;
	public float speed;
	public float friction;
	//Angles
	public float rotSpeed;
	public float jumpSpeed;
	public Vector3 gravity;
	[SerializeField] Vector3 velocity;

	void Update () {
		if (controller.isGrounded) {
			velocity.y = 0;
		}
		animController.SetFloat ("Vertical", velocity.y);

	}

	private void FixedUpdate () {
		Vector2 dir = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		animController.SetFloat ("Forward", dir.y);
		animController.SetFloat ("Direction", dir.x);
		velocity += gravity;
		velocity += transform.forward * (dir.y * speed);

		if (Input.GetButtonDown ("Jump") && controller.isGrounded) {
			velocity.y += jumpSpeed;
			animController.SetTrigger ("Jump");
		}
		velocity -= velocity * friction;

		transform.Rotate (new Vector3 (0, dir.x, 0) * (rotSpeed * Time.deltaTime));
		controller.Move (velocity * Time.deltaTime);
	}

	void CheckIfGrounded () {

	}
}