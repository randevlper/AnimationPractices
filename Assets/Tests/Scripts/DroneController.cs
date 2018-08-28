using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
	public Transform target;
	public float speed;
	private Camera mainCamera;
	[SerializeField] Rigidbody rb;
	private void Start () {
		mainCamera = Camera.main;
		rb.useGravity = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate() {
		transform.forward = mainCamera.transform.forward;
		rb.velocity = (Vector3.Distance (target.position, transform.position) * speed) * Vector3.Lerp (rb.velocity, (target.position - transform.position).normalized, 1.0f);
	}
}