using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gold;

public class BulletEmmiter : MonoBehaviour {
	public GameObject bulletPrefab;
	protected ObjectPool bulletPool;
	public float speed;
	public float cooldown;
	public bool canShoot;
	protected virtual void Start () {
		bulletPool = new ObjectPool (bulletPrefab, 10, true);
		canShoot = true;
	}
	virtual public void  Emit () {
		if (canShoot) {
			Bullet bul = bulletPool.Get ().GetComponent<Bullet> ();
			if (bul != null) {
				bul.gameObject.SetActive (true);
				bul.Shoot (transform.position, speed, transform.forward);//(pos + (Vector3)Random.insideUnitCircle - transform.position).normalized);
				StartCoroutine (Wait (cooldown));
			}
			//(pos - transform.position)
		}
	}

	protected IEnumerator Wait (float value) {
		canShoot = false;
		yield return new WaitForSeconds (value);
		canShoot = true;
	}
}