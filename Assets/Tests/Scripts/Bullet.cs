using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField] private Rigidbody rb;

	public void Shoot(float speed, Vector3 dir){
		transform.forward = dir;
		rb.velocity = dir * speed;
	}
}
