using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
	public Transform target;
	public float speed;
	private Camera mainCamera;
	private void Start() {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position,target.position,speed);
		transform.forward = mainCamera.transform.forward;
	}
}
