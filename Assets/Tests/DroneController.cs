﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
	public Transform target;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position,target.position,speed);
	}
}
