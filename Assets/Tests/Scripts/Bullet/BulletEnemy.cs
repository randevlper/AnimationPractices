using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet {
	[SerializeField] DamageableBody damageableBody;

	// Use this for initialization
	void Start () {
		damageableBody.onDamage += OnDamage;
	}

	void OnDamage (HitData hit) {
		gameObject.SetActive (false);
	}
}