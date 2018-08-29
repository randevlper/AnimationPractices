using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	[SerializeField] BulletEmmiterController controller;
	Camera mainCamera;
	public LayerMask shootMask;
	public float maxRaycastDistance;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}

	// Update is called once per frame
	Ray ray;
	RaycastHit hit;
	void Update () {
		if (Input.GetButton ("Fire1")) {
			controller.ShootAll ();

			
		}
	}
}