using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class BulletEmmiterCircle : BulletEmmiter {

	public int bulletsPerBurst;

	[Range (-360, 360)]
	public int startAngle;
	[Range (-360, 360)]
	public int endAngle;

	protected override void Start () {
		base.Start ();
	}

	public override void Emit () {
		if (canShoot) {
			for (int i = 0; i < bulletsPerBurst; i++) {
				GameObject bulletObject = bulletPool.Get ();
				Bullet bullet = bulletObject.GetComponent<Bullet> ();

				float angle = (((endAngle - startAngle) / bulletsPerBurst) * i) + startAngle;
				Vector2 dir = MathG.DegreeToVector2 (angle);
				//Debug.Log (angle + " " + dir);
				bullet.gameObject.SetActive (true);
				bullet.Shoot (transform.position, speed, (new Vector3 (dir.x, 0, dir.y)).normalized);
			}
			StartCoroutine (Wait (cooldown));
		}
	}
}