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
			ray = mainCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f));
			if(Physics.Raycast (ray, out hit, 100, shootMask)){
				Debug.DrawRay(ray.origin,ray.direction,Color.red);
				controller.ShootAll (hit.point);
			}

			
		}
	}
}