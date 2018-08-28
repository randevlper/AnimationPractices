using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField] private Rigidbody rb;

	public void Shoot(Vector3 startPos, float speed, Vector3 dir){
		transform.position = startPos;
		transform.forward = dir;
		rb.velocity = dir * speed;
	}

	private void OnCollisionEnter(Collision other) {
		gameObject.SetActive(false);
	}
}
