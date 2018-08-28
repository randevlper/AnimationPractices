using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class BulletEmmiter : MonoBehaviour {
	public GameObject bulletPrefab;
	ObjectPool bulletPool;
	public float speed;
	public float cooldown;
	public bool canShoot;

	private void Start () {
		bulletPool = new ObjectPool (bulletPrefab, 10, true);
		canShoot = true;
	}
	public void Emit (Vector3 pos) {
		if (canShoot) {
			Bullet bul = bulletPool.Get ().GetComponent<Bullet> ();
			if (bul != null) {
				bul.gameObject.SetActive (true);
				bul.Shoot (transform.position, speed, (pos + (Vector3)Random.insideUnitCircle - transform.position).normalized);
				StartCoroutine (Wait (cooldown));
			}
			//(pos - transform.position)
		}
	}

	IEnumerator Wait (float value) {
		canShoot = false;
		yield return new WaitForSeconds (value);
		canShoot = true;
	}
}