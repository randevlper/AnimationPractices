using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gold;
public class BulletEmitterForward : BulletEmmiter {

	[Range (0f, 1.0f)]
	public float accuracy;
	public override void Emit () {
		if (canShoot) {
			Bullet bul = bulletPool.Get ().GetComponent<Bullet> ();
			if (bul != null) {
				bul.gameObject.SetActive (true);
				bul.Shoot (transform.position, speed, transform.forward + (Random.insideUnitSphere * accuracy)); //(pos + (Vector3)Random.insideUnitCircle - transform.position).normalized);
				StartCoroutine (Wait (cooldown));
			}
			//(pos - transform.position)
		}
	}
}