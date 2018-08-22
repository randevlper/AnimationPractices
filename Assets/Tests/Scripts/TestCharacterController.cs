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
		Vector3 leftStick = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0 , Input.GetAxisRaw ("Vertical"));
		//Vector2 rightStick = Input.mouseScrollDelta;

		

		Vector3 charDir = (mainCamera.transform.forward * leftStick.z) + (mainCamera.transform.right * leftStick.x);
		Debug.DrawRay(transform.position + new Vector3(0,2),charDir * 10f);

		//velocity +=  transform.forward * (speed * charDir.magnitude); 
		velocity = velocity + (charDir.normalized * speed);


		velocity += gravity;
		velocity -= velocity * friction;


		animController.SetFloat ("Forward", charDir.magnitude);
		animController.SetFloat ("Direction", charDir.x);
		controller.Move (velocity * Time.deltaTime);
	}

	void Jump () {
		if (Input.GetButtonDown ("Jump") && controller.isGrounded) {
			velocity.y += jumpSpeed;
			animController.SetTrigger ("Jump");
		}
	}

	void CheckIfGrounded () {

	}
}